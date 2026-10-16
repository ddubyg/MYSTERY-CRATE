namespace MysteryCrate.Services;

using MysteryCrate.Models;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items;

    public int TotalCount => _items.Sum(i => i.Quantity);

    public decimal Subtotal => _items.Sum(i => i.Total);

    public decimal ShippingFee => Subtotal > 1500m || Subtotal == 0m ? 0m : 50m;

    public decimal Total => Subtotal + ShippingFee;

    public void AddItem(CartItem item)
    {
        var existing = _items.FirstOrDefault(i => i.ReferenceId == item.ReferenceId && i.Type == item.Type);
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            _items.Add(item);
        }
        NotifyStateChanged();
    }

    public void AddCreature(Creature creature, int quantity = 1)
    {
        AddItem(new CartItem
        {
            Type = CartItemType.CreatureCard,
            ReferenceId = creature.Id,
            Title = creature.Name,
            Subtitle = $"{creature.Rarity} • {creature.CreatureType}",
            UnitPrice = creature.GetValuation(),
            Quantity = quantity,
            Emoji = creature.EmojiArt,
            Badge = creature.Rarity.ToString()
        });
    }

    public void AddPack(MysteryPack pack, int quantity = 1)
    {
        AddItem(new CartItem
        {
            Type = CartItemType.MysteryPack,
            ReferenceId = pack.Id,
            Title = pack.Name,
            Subtitle = pack.Subtitle,
            UnitPrice = pack.Price,
            Quantity = quantity,
            Emoji = pack.IconEmoji,
            Badge = pack.BadgeText
        });
    }

    public void RemoveItem(string cartItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item != null)
        {
            _items.Remove(item);
            NotifyStateChanged();
        }
    }

    public void UpdateQuantity(string cartItemId, int newQuantity)
    {
        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item != null)
        {
            if (newQuantity <= 0)
            {
                _items.Remove(item);
            }
            else
            {
                item.Quantity = newQuantity;
            }
            NotifyStateChanged();
        }
    }

    public void ClearCart()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

