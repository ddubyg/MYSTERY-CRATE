namespace MysteryCrate.Models;

public enum OpeningOutcome
{
    Loss,
    BreakEven,
    Profit,
    Jackpot
}

public class CrateDropTable
{
    public Dictionary<RarityTier, double> Weights { get; set; } = new();
    public double MutationChance { get; set; } = 0.35;

    public RarityTier RollRarity(Random rng)
    {
        if (Weights.Count == 0) return RarityTier.Common;

        double totalWeight = Weights.Values.Sum();
        if (totalWeight <= 0) return RarityTier.Common;

        double roll = rng.NextDouble() * totalWeight;
        double cumulative = 0.0;

        foreach (var kvp in Weights)
        {
            cumulative += kvp.Value;
            if (roll <= cumulative)
            {
                return kvp.Key;
            }
        }

        return Weights.Keys.Last();
    }
}

public class RevealedCard
{
    public Creature Creature { get; set; } = null!;
    public string DisplayMutation { get; set; } = string.Empty;
    public decimal Valuation { get; set; }
    public int SlotNumber { get; set; }
    public bool IsRevealed { get; set; } = false;
    public bool IsFinalCard { get; set; } = false;
}

public class CrateOpeningResult
{
    public string OpeningId { get; set; } = $"OPN-{Random.Shared.Next(10000, 99999)}";
    public string PackId { get; set; } = string.Empty;
    public string PackName { get; set; } = string.Empty;
    public decimal CrateCost { get; set; }
    public List<RevealedCard> Cards { get; set; } = new();
    public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
    public OpeningOutcome Outcome { get; set; }
    public bool VaultCommitted { get; set; } = false;

    public decimal TotalCardValue => Cards.Sum(c => c.Valuation);
    public decimal NetDifference => TotalCardValue - CrateCost;

    public decimal NetPercentage => CrateCost > 0
        ? Math.Round(((TotalCardValue - CrateCost) / CrateCost) * 100m, 1)
        : 0m;
}

public class OpeningHistoryEntry
{
    public string Id { get; set; } = string.Empty;
    public string PackName { get; set; } = string.Empty;
    public decimal CrateCost { get; set; }
    public int CardCount { get; set; }
    public decimal TotalValue { get; set; }
    public decimal NetDifference { get; set; }
    public OpeningOutcome Outcome { get; set; }
    public DateTime Timestamp { get; set; }
    public List<RevealedCard> Cards { get; set; } = new();
}

