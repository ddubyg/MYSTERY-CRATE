namespace MysteryCrate.Models;

public class MysteryPack
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "📦";
    public string BadgeText { get; set; } = "Standard";
    public string BadgeColor { get; set; } = "bg-purple-light text-purple-muted";
    public string BoxGradient { get; set; } = "from-plum-deep via-plum-secondary to-purple-muted";
    public string OddsDescription { get; set; } = string.Empty;
    public RarityTier MinGuaranteedRarity { get; set; } = RarityTier.Common;
    public List<string> CreaturePoolIds { get; set; } = new();
}

