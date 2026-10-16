namespace MysteryCrate.Models;

public class Listing
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string CreatureId { get; set; } = string.Empty;
    public Creature Creature { get; set; } = null!;
    public string SellerName { get; set; } = "Anonymous Collector";
    public string SellerBadge { get; set; } = "Verified Trader";
    public decimal AskingPrice { get; set; }
    public DateTime ListedAt { get; set; } = DateTime.UtcNow.AddHours(-12);
    public string Notes { get; set; } = "Mint condition, freshly unboxed.";
    public bool IsSold { get; set; } = false;
}

