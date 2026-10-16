namespace MysteryCrate.Services;

using MysteryCrate.Data;
using MysteryCrate.Models;

public class MarketplaceService
{
    private readonly List<Listing> _listings;
    private readonly CollectionService _collectionService;

    public event Action? OnChange;

    public MarketplaceService(CreatureService creatureService, CollectionService collectionService)
    {
        _collectionService = collectionService;
        _listings = SampleData.GetDefaultListings(creatureService.GetAllCreatures().ToList());
    }

    public IReadOnlyList<Listing> GetActiveListings() => _listings.Where(l => !l.IsSold).OrderByDescending(l => l.ListedAt).ToList();

    public Listing? GetListingById(string id) => _listings.FirstOrDefault(l => l.Id == id);

    public (bool Success, string Message) BuyListing(string listingId)
    {
        var listing = GetListingById(listingId);
        if (listing == null)
        {
            return (false, "Listing was not found.");
        }

        if (listing.IsSold)
        {
            return (false, "This card has already been acquired by another collector.");
        }

        listing.IsSold = true;
        _collectionService.AddCard(listing.Creature, 1);
        NotifyStateChanged();

        return (true, $"Simulated purchase successful! {listing.Creature.Name} has been deposited to your Vault.");
    }

    public (bool Success, string Message, Listing? Listing) CreateListing(string creatureId, decimal askingPrice, string sellerName, string notes)
    {
        var entry = _collectionService.GetEntry(creatureId);
        if (entry == null || entry.Quantity <= 0)
        {
            return (false, "You do not own this card to list on The Exchange.", null);
        }

        if (askingPrice <= 0)
        {
            return (false, "Asking price must be greater than zero.", null);
        }

        bool removed = _collectionService.RemoveCard(creatureId, 1);
        if (!removed)
        {
            return (false, "Could not reserve card from your vault.", null);
        }

        var newListing = new Listing
        {
            CreatureId = entry.Creature.Id,
            Creature = entry.Creature,
            SellerName = string.IsNullOrWhiteSpace(sellerName) ? "You (Vault Owner)" : sellerName.Trim(),
            SellerBadge = "Player Listing",
            AskingPrice = askingPrice,
            Notes = string.IsNullOrWhiteSpace(notes) ? "Listed from player personal vault." : notes.Trim(),
            ListedAt = DateTime.UtcNow
        };

        _listings.Insert(0, newListing);
        NotifyStateChanged();

        return (true, $"Successfully listed {entry.Creature.Name} for ₱{askingPrice:N0} on The Exchange!", newListing);
    }

    public IEnumerable<Listing> FilterListings(string? search = null, RarityTier? rarity = null, string? sortBy = null)
    {
        var query = _listings.Where(l => !l.IsSold);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(l =>
                l.Creature.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                l.Creature.CreatureType.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                l.SellerName.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        if (rarity.HasValue)
        {
            query = query.Where(l => l.Creature.Rarity == rarity.Value);
        }

        query = sortBy switch
        {
            "price-asc" => query.OrderBy(l => l.AskingPrice),
            "price-desc" => query.OrderByDescending(l => l.AskingPrice),
            "name-asc" => query.OrderBy(l => l.Creature.Name),
            "date-newest" => query.OrderByDescending(l => l.ListedAt),
            _ => query.OrderByDescending(l => l.ListedAt)
        };

        return query.ToList();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

