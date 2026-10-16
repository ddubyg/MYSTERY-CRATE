namespace MysteryCrate.Services;

using MysteryCrate.Data;
using MysteryCrate.Models;

public class TradeService
{
    private readonly List<TradePartner> _partners;
    private readonly CreatureService _creatureService;
    private readonly CollectionService _collectionService;
    private readonly List<TradeHistoryRecord> _history = new();

    public event Action? OnChange;

    public IReadOnlyList<TradePartner> Partners => _partners;
    public IReadOnlyList<TradeHistoryRecord> History => _history;

    public TradeService(CreatureService creatureService, CollectionService collectionService)
    {
        _creatureService = creatureService;
        _collectionService = collectionService;
        _partners = SampleData.GetDefaultTradePartners();
    }

    public TradePartner? GetPartnerById(string id) => _partners.FirstOrDefault(p => p.Id == id);

    public List<Creature> GetPartnerInventory(TradePartner partner)
    {
        return partner.InventoryCreatureIds
            .Select(id => _creatureService.GetCreatureById(id))
            .Where(c => c != null)
            .Cast<Creature>()
            .ToList();
    }

    public (bool IsValid, string Message) ValidateTrade(TradePartner partner, List<TradeSelectionItem> offered, List<TradeSelectionItem> requested)
    {
        if (offered.Count == 0)
        {
            return (false, "Please select at least one card from your vault to offer.");
        }

        if (requested.Count == 0)
        {
            return (false, $"Please select at least one card from {partner.Name}'s inventory to request.");
        }

        // Verify user owns all offered cards and sufficient quantities
        foreach (var item in offered)
        {
            var entry = _collectionService.GetEntry(item.Creature.Id);
            if (entry == null || entry.Quantity < item.Quantity)
            {
                return (false, $"You only have {entry?.Quantity ?? 0}x of {item.Creature.Name} in your vault.");
            }
        }

        decimal totalOfferedVal = offered.Sum(o => o.Creature.GetValuation() * o.Quantity);
        decimal totalRequestedVal = requested.Sum(r => r.Creature.GetValuation() * r.Quantity);

        // Partner evaluates deal: partner accepts if offered is at least 70% of requested value
        if (totalOfferedVal < totalRequestedVal * 0.70m)
        {
            return (false, $"{partner.Name} considers this offer too uneven. (Offered value ₱{totalOfferedVal:N0} vs Requested value ₱{totalRequestedVal:N0}). Add more value to balance the trade.");
        }

        return (true, $"{partner.Name} is pleased with this offer! Trade terms are fair.");
    }

    public (bool Success, string Message) ExecuteTrade(TradePartner partner, List<TradeSelectionItem> offered, List<TradeSelectionItem> requested)
    {
        var validation = ValidateTrade(partner, offered, requested);
        if (!validation.IsValid)
        {
            return (false, validation.Message);
        }

        // Deduct offered from user vault
        foreach (var item in offered)
        {
            _collectionService.RemoveCard(item.Creature.Id, item.Quantity);
        }

        // Add requested to user vault
        foreach (var item in requested)
        {
            _collectionService.AddCard(item.Creature, item.Quantity);
        }

        // Record history
        var record = new TradeHistoryRecord
        {
            PartnerName = partner.Name,
            GivenCards = offered.Select(o => new TradeSelectionItem { Creature = o.Creature, Quantity = o.Quantity }).ToList(),
            ReceivedCards = requested.Select(r => new TradeSelectionItem { Creature = r.Creature, Quantity = r.Quantity }).ToList(),
            CompletedAt = DateTime.UtcNow,
            SummaryText = $"Traded {offered.Sum(o => o.Quantity)} card(s) for {requested.Sum(r => r.Quantity)} card(s) with {partner.Name}."
        };
        _history.Insert(0, record);

        NotifyStateChanged();
        return (true, $"Trade successfully completed with {partner.Name}! Your new cards have been added to your Vault.");
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

