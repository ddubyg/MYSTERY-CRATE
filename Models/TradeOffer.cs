namespace MysteryCrate.Models;

public class TradePartner
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string AvatarEmoji { get; set; } = "🧙‍♂️";
    public string Bio { get; set; } = string.Empty;
    public List<string> InventoryCreatureIds { get; set; } = new();
}

public class TradeSelectionItem
{
    public Creature Creature { get; set; } = null!;
    public int Quantity { get; set; } = 1;
}

public class TradeHistoryRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string PartnerName { get; set; } = string.Empty;
    public List<TradeSelectionItem> GivenCards { get; set; } = new();
    public List<TradeSelectionItem> ReceivedCards { get; set; } = new();
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    public string SummaryText { get; set; } = string.Empty;
}

