namespace MysteryCrate.Services;

using MysteryCrate.Data;
using MysteryCrate.Models;

public class MysteryPackService
{
    private readonly List<MysteryPack> _packs;
    private readonly CreatureService _creatureService;
    private readonly CollectionService _collectionService;

    public MysteryPackService(CreatureService creatureService, CollectionService collectionService)
    {
        _creatureService = creatureService;
        _collectionService = collectionService;
        _packs = SampleData.GetDefaultMysteryPacks();
    }

    public IReadOnlyList<MysteryPack> GetPacks() => _packs;

    public MysteryPack? GetPackById(string id)
    {
        return _packs.FirstOrDefault(p => string.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    public Creature OpenPack(string packId)
    {
        var pack = GetPackById(packId) ?? _packs.First();

        var eligibleCreatures = pack.CreaturePoolIds
            .Select(id => _creatureService.GetCreatureById(id))
            .Where(c => c != null && c.Rarity >= pack.MinGuaranteedRarity)
            .Cast<Creature>()
            .ToList();

        if (eligibleCreatures.Count == 0)
        {
            eligibleCreatures = _creatureService.GetAllCreatures()
                .Where(c => c.Rarity >= pack.MinGuaranteedRarity && !c.IsFeaturedShowcase)
                .ToList();
        }

        // Weighted pick: higher rarities are rarer
        // Weights: Common 50, Uncommon 30, Rare 15, Epic 8, Mythic 4, Legendary 2, Celestial 1
        var weightedPool = new List<Creature>();
        foreach (var c in eligibleCreatures)
        {
            int weight = c!.Rarity switch
            {
                RarityTier.Common => 50,
                RarityTier.Uncommon => 30,
                RarityTier.Rare => 15,
                RarityTier.Epic => 8,
                RarityTier.Mythic => 4,
                RarityTier.Legendary => 2,
                RarityTier.Celestial => 1,
                _ => 10
            };

            for (int i = 0; i < weight; i++)
            {
                weightedPool.Add(c);
            }
        }

        var picked = weightedPool[Random.Shared.Next(weightedPool.Count)];

        // Add to collection
        _collectionService.AddCard(picked, 1);

        return picked;
    }
}
