namespace MysteryCrate.Services;

using MysteryCrate.Data;
using MysteryCrate.Models;

public class VaultCardEntry
{
    public Creature Creature { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public DateTime FirstAcquired { get; set; } = DateTime.UtcNow;
}

public class CollectionService
{
    private readonly Dictionary<string, VaultCardEntry> _userVault = new();

    public event Action? OnChange;

    public CollectionService(CreatureService creatureService)
    {
        // Seed initial starter collection for user demo experience
        var initialIds = new[] { "gloomspore-002", "tidepool-003", "cinder-fox-004", "zephyr-005", "obsidian-006" };
        foreach (var id in initialIds)
        {
            var creature = creatureService.GetCreatureById(id);
            if (creature != null)
            {
                _userVault[creature.Id] = new VaultCardEntry
                {
                    Creature = creature,
                    Quantity = 1,
                    FirstAcquired = DateTime.UtcNow.AddDays(-2)
                };
            }
        }
    }

    public IReadOnlyList<VaultCardEntry> GetOwnedCards() => _userVault.Values.OrderByDescending(v => v.Creature.Rarity).ToList();

    public VaultCardEntry? GetEntry(string creatureId)
    {
        return _userVault.TryGetValue(creatureId, out var entry) ? entry : null;
    }

    public int GetTotalCardCount() => _userVault.Values.Sum(v => v.Quantity);

    public int GetUniqueCount() => _userVault.Count;

    public decimal GetTotalCollectionValuation()
    {
        return _userVault.Values.Sum(v => v.Creature.GetValuation() * v.Quantity);
    }

    public Dictionary<RarityTier, int> GetRarityBreakdown()
    {
        var breakdown = new Dictionary<RarityTier, int>();
        foreach (RarityTier tier in Enum.GetValues<RarityTier>())
        {
            breakdown[tier] = 0;
        }

        foreach (var entry in _userVault.Values)
        {
            breakdown[entry.Creature.Rarity] += entry.Quantity;
        }

        return breakdown;
    }

    public int GetTotalMutationsCount()
    {
        return _userVault.Values.Sum(v => v.Creature.Mutations.Count * v.Quantity);
    }

    public void AddCard(Creature creature, int quantity = 1)
    {
        if (_userVault.TryGetValue(creature.Id, out var entry))
        {
            entry.Quantity += quantity;
        }
        else
        {
            _userVault[creature.Id] = new VaultCardEntry
            {
                Creature = creature,
                Quantity = quantity,
                FirstAcquired = DateTime.UtcNow
            };
        }
        NotifyStateChanged();
    }

    public bool RemoveCard(string creatureId, int quantity = 1)
    {
        if (_userVault.TryGetValue(creatureId, out var entry))
        {
            if (entry.Quantity > quantity)
            {
                entry.Quantity -= quantity;
                NotifyStateChanged();
                return true;
            }
            else if (entry.Quantity == quantity)
            {
                _userVault.Remove(creatureId);
                NotifyStateChanged();
                return true;
            }
        }
        return false;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

