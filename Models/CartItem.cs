namespace MysteryCrate.Models;

public enum CartItemType
{
    CreatureCard,
    MysteryPack,
    MarketplaceListing
}

public class CartItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public CartItemType Type { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; } = 1;
    public string Emoji { get; set; } = "🎴";
    public string Badge { get; set; } = string.Empty;

    public decimal Total => UnitPrice * Quantity;
}

