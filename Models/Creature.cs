namespace MysteryCrate.Models;

public class Creature
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string CreatureType { get; set; } = string.Empty;
    public string Habitat { get; set; } = string.Empty;
    public RarityTier Rarity { get; set; } = RarityTier.Common;
    public decimal BasePrice { get; set; }
    public List<string> Mutations { get; set; } = new();
    public string EmojiArt { get; set; } = "🎴";
    public string ArtColorGradient { get; set; } = "from-plum-deep to-plum-secondary";
    public string Description { get; set; } = string.Empty;
    public string? SerialNumber { get; set; }
    public bool IsOneOfOne => SerialNumber == "001 / 001";
    public bool IsFeaturedShowcase { get; set; }
    public decimal? CustomValuation { get; set; }
    public bool IsAvailableInShop { get; set; } = true;

    /// <summary>
    /// Valuation calculation rule:
    /// If CustomValuation is set (like Aurex ₱5,000,000 showcase), use it.
    /// Otherwise, calculated deterministically: BasePrice * Product(Mutation multipliers).
    /// </summary>
    public decimal GetValuation()
    {
        if (CustomValuation.HasValue)
        {
            return CustomValuation.Value;
        }

        decimal mult = 1.0m;
        foreach (var m in Mutations)
        {
            mult *= MutationHelper.GetMultiplier(m);
        }

        // Round to nearest integer for clean aesthetic display
        return Math.Round(BasePrice * mult, 0);
    }
}

