namespace MysteryCrate.Services;

using MysteryCrate.Data;
using MysteryCrate.Models;

public class MysteryPackService
{
    private readonly List<MysteryPack> _packs;
    private readonly CreatureService _creatureService;
    private readonly CollectionService _collectionService;
    private readonly List<OpeningHistoryEntry> _history = new();

    public event Action? OnChange;

    public MysteryPackService(CreatureService creatureService, CollectionService collectionService)
    {
        _creatureService = creatureService;
        _collectionService = collectionService;
        _packs = SampleData.GetDefaultMysteryPacks();
    }

    public IReadOnlyList<MysteryPack> GetPacks() => _packs;

    public IReadOnlyList<OpeningHistoryEntry> History => _history;

    public MysteryPack? GetPackById(string id)
    {
        return _packs.FirstOrDefault(p => string.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Generates a complete, multi-card crate opening result deterministically before any reveal begins.
    /// Every crate contains at least five cards per opening.
    /// </summary>
    public CrateOpeningResult GenerateCrateOpening(string packId)
    {
        var pack = GetPackById(packId) ?? _packs.First();
        var allCreatures = _creatureService.GetAllCreatures();

        int cardCount = Math.Max(5, pack.CardCount);
        var revealedCards = new List<RevealedCard>();
        var rng = Random.Shared;

        for (int i = 0; i < cardCount; i++)
        {
            // 1. Select rarity using configured weighted drop table
            RarityTier chosenRarity = pack.DropTable.RollRarity(rng);

            // 2. Select eligible cards for that rarity (handling empty pools safely with fallback)
            var pool = allCreatures.Where(c => c.Rarity == chosenRarity && !c.IsFeaturedShowcase).ToList();
            if (pool.Count == 0)
            {
                // Fallback: pick nearest rarity pool
                pool = allCreatures.Where(c => !c.IsFeaturedShowcase).ToList();
            }

            var baseCreature = pool[rng.Next(pool.Count)];

            // Clone creature instance so we can apply rolled mutations safely without mutating static catalog
            var cardInstance = new Creature
            {
                Id = baseCreature.Id,
                Name = baseCreature.Name,
                Title = baseCreature.Title,
                CreatureType = baseCreature.CreatureType,
                Habitat = baseCreature.Habitat,
                Rarity = baseCreature.Rarity,
                BasePrice = baseCreature.BasePrice,
                Mutations = new List<string>(baseCreature.Mutations),
                EmojiArt = baseCreature.EmojiArt,
                ArtColorGradient = baseCreature.ArtColorGradient,
                Description = baseCreature.Description,
                SerialNumber = baseCreature.SerialNumber,
                IsFeaturedShowcase = baseCreature.IsFeaturedShowcase,
                CustomValuation = baseCreature.CustomValuation,
                IsAvailableInShop = baseCreature.IsAvailableInShop
            };

            // 3. Roll extra mutation based on configured mutation chance
            if (rng.NextDouble() < pack.DropTable.MutationChance && MutationHelper.AllMutations.Length > 0)
            {
                string extraMutation = MutationHelper.AllMutations[rng.Next(MutationHelper.AllMutations.Length)];
                if (!cardInstance.Mutations.Contains(extraMutation, StringComparer.OrdinalIgnoreCase))
                {
                    cardInstance.Mutations.Add(extraMutation);
                }
            }

            revealedCards.Add(new RevealedCard
            {
                Creature = cardInstance,
                DisplayMutation = string.Join(", ", cardInstance.Mutations),
                Valuation = cardInstance.GetValuation(),
                SlotNumber = i + 1,
                IsRevealed = false,
                IsFinalCard = (i == cardCount - 1)
            });
        }

        var result = new CrateOpeningResult
        {
            PackId = pack.Id,
            PackName = pack.Name,
            CrateCost = pack.Price,
            Cards = revealedCards,
            OpenedAt = DateTime.UtcNow,
            VaultCommitted = false
        };

        // Determine final outcome category
        // JACKPOT condition: contains Celestial card OR Aurex OR Total value >= 2.5x crate cost
        if (revealedCards.Any(c => c.Creature.Rarity == RarityTier.Celestial || c.Creature.Id == "aurex-001") ||
            (result.TotalCardValue >= result.CrateCost * 2.5m && result.TotalCardValue > result.CrateCost))
        {
            result.Outcome = OpeningOutcome.Jackpot;
        }
        else if (result.TotalCardValue > result.CrateCost)
        {
            result.Outcome = OpeningOutcome.Profit;
        }
        else if (result.TotalCardValue == result.CrateCost)
        {
            result.Outcome = OpeningOutcome.BreakEven;
        }
        else
        {
            result.Outcome = OpeningOutcome.Loss;
        }

        return result;
    }

    /// <summary>
    /// Idempotently commits all cards from an opening to the user's vault and records history.
    /// Prevents duplicate processing on UI rerenders.
    /// </summary>
    public void CommitOpeningToVault(CrateOpeningResult result)
    {
        if (result.VaultCommitted) return;

        result.VaultCommitted = true;

        foreach (var card in result.Cards)
        {
            _collectionService.AddCard(card.Creature, 1);
        }

        var entry = new OpeningHistoryEntry
        {
            Id = result.OpeningId,
            PackName = result.PackName,
            CrateCost = result.CrateCost,
            CardCount = result.Cards.Count,
            TotalValue = result.TotalCardValue,
            NetDifference = result.NetDifference,
            Outcome = result.Outcome,
            Timestamp = result.OpenedAt,
            Cards = result.Cards.ToList()
        };

        _history.Insert(0, entry);
        NotifyStateChanged();
    }

    /// <summary>
    /// Backwards-compatible single-pack open helper used by cart/checkout.
    /// Opens the 5-card crate and deposits all cards to vault, returning the highest-value card.
    /// </summary>
    public Creature OpenPack(string packId)
    {
        var result = GenerateCrateOpening(packId);
        CommitOpeningToVault(result);
        return result.Cards.OrderByDescending(c => c.Valuation).First().Creature;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
