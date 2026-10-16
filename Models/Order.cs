namespace MysteryCrate.Models;

public class Order
{
    public string Id { get; set; } = $"MC-{Random.Shared.Next(10000, 99999)}";
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string SimulatedPaymentNote { get; set; } = "Simulated Sandbox Demo Transaction - No real money charged.";
    public List<CartItem> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal Discount { get; set; }
    public decimal Total => Math.Max(0, Subtotal + ShippingFee - Discount);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Completed (Simulated)";
}

