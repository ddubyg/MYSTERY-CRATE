namespace MysteryCrate.Services;

using MysteryCrate.Data;
using MysteryCrate.Models;

public class CreatureService
{
    private readonly List<Creature> _creatures;

    public CreatureService()
    {
        _creatures = SampleData.GetAllCreatures();
    }

    public IReadOnlyList<Creature> GetAllCreatures() => _creatures;

    public Creature? GetCreatureById(string id)
    {
        return _creatures.FirstOrDefault(c => string.Equals(c.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    public Creature GetFeaturedAurex()
    {
        return _creatures.First(c => c.Id == "aurex-001");
    }

    public IEnumerable<Creature> FilterCreatures(
        string? search = null,
        RarityTier? rarity = null,
        string? mutation = null,
        string? sortBy = null)
    {
        var query = _creatures.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(c =>
                c.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                c.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                c.CreatureType.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                c.Habitat.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        if (rarity.HasValue)
        {
            query = query.Where(c => c.Rarity == rarity.Value);
        }

        if (!string.IsNullOrWhiteSpace(mutation) && mutation != "All")
        {
            query = query.Where(c => c.Mutations.Any(m => string.Equals(m, mutation, StringComparison.OrdinalIgnoreCase)));
        }

        query = sortBy switch
        {
            "name-asc" => query.OrderBy(c => c.Name),
            "name-desc" => query.OrderByDescending(c => c.Name),
            "price-asc" => query.OrderBy(c => c.GetValuation()),
            "price-desc" => query.OrderByDescending(c => c.GetValuation()),
            "rarity-desc" => query.OrderByDescending(c => c.Rarity).ThenByDescending(c => c.GetValuation()),
            "rarity-asc" => query.OrderBy(c => c.Rarity).ThenBy(c => c.GetValuation()),
            _ => query.OrderByDescending(c => c.Rarity).ThenBy(c => c.Name)
        };

        return query.ToList();
    }
}

