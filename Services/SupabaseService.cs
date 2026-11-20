namespace MysteryCrate.Services;

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MysteryCrate.Models;

public class SupabaseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger<SupabaseService> _logger;

    private readonly string _url;
    private readonly string _anonKey;

    public bool IsConfigured { get; }

    public SupabaseService(HttpClient httpClient, IConfiguration config, ILogger<SupabaseService> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;

        _url = _config["Supabase:Url"]?.TrimEnd('/') ?? string.Empty;
        _anonKey = _config["Supabase:AnonKey"] ?? string.Empty;

        IsConfigured = !string.IsNullOrWhiteSpace(_url) 
                       && !_url.Contains("YOUR_PROJECT_REF", StringComparison.OrdinalIgnoreCase)
                       && !string.IsNullOrWhiteSpace(_anonKey)
                       && !_anonKey.Contains("YOUR_SUPABASE_ANON_KEY", StringComparison.OrdinalIgnoreCase);

        if (IsConfigured)
        {
            _httpClient.BaseAddress = new Uri(_url);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _anonKey);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _anonKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger.LogInformation("SupabaseService initialized with endpoint: {Url}", _url);
        }
        else
        {
            _logger.LogInformation("Supabase is not configured. Falling back to local in-memory dataset.");
        }
    }

    /// <summary>
    /// Test connection by querying 1 record from the creatures table.
    /// </summary>
    public async Task<(bool Success, string Message)> TestConnectionAsync()
    {
        if (!IsConfigured)
        {
            return (false, "Supabase credentials are not set in appsettings.json. Operating in local mode.");
        }

        try
        {
            var response = await _httpClient.GetAsync("/rest/v1/creatures?select=id&limit=1");
            if (response.IsSuccessStatusCode)
            {
                return (true, $"Successfully connected to Supabase project at {_url}!");
            }

            var err = await response.Content.ReadAsStringAsync();
            return (false, $"Supabase returned HTTP {(int)response.StatusCode}: {err}");
        }
        catch (Exception ex)
        {
            return (false, $"Connection error: {ex.Message}");
        }
    }

    /// <summary>
    /// Fetch creatures catalog from Supabase.
    /// </summary>
    public async Task<List<Creature>?> GetCreaturesAsync()
    {
        if (!IsConfigured) return null;

        try
        {
            var response = await _httpClient.GetAsync("/rest/v1/creatures?select=*&order=base_price.desc");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var dtos = JsonSerializer.Deserialize<List<SupabaseCreatureDto>>(json, JsonOptions);
            return dtos?.Select(d => d.ToDomainModel()).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch creatures from Supabase, using local fallback.");
            return null;
        }
    }

    /// <summary>
    /// Record a completed crate opening in Supabase history.
    /// </summary>
    public async Task<bool> RecordCrateOpeningAsync(CrateOpeningResult result)
    {
        if (!IsConfigured) return false;

        try
        {
            var payload = new
            {
                id = result.OpeningId,
                pack_name = result.PackName,
                crate_cost = result.CrateCost,
                card_count = result.Cards.Count,
                total_value = result.TotalCardValue,
                net_difference = result.NetDifference,
                outcome = result.Outcome.ToString(),
                timestamp = result.OpenedAt,
                cards_summary = result.Cards.Select(c => new
                {
                    id = c.Creature.Id,
                    name = c.Creature.Name,
                    rarity = c.Creature.Rarity.ToString(),
                    valuation = c.Valuation
                })
            };

            var content = new StringContent(JsonSerializer.Serialize(payload, JsonOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/rest/v1/crate_opening_history", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to persist crate opening to Supabase.");
            return false;
        }
    }

    /// <summary>
    /// Record an order in Supabase.
    /// </summary>
    public async Task<bool> RecordOrderAsync(Order order)
    {
        if (!IsConfigured) return false;

        try
        {
            var payload = new
            {
                id = order.Id,
                customer_name = order.CustomerName,
                email = order.Email,
                shipping_address = order.ShippingAddress,
                city = order.City,
                postal_code = order.PostalCode,
                subtotal = order.Subtotal,
                shipping_fee = order.ShippingFee,
                total = order.Total,
                simulated_payment_note = order.SimulatedPaymentNote,
                items_json = order.Items.Select(i => new
                {
                    title = i.Title,
                    quantity = i.Quantity,
                    unit_price = i.UnitPrice,
                    total = i.Total
                })
            };

            var content = new StringContent(JsonSerializer.Serialize(payload, JsonOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/rest/v1/orders", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to persist order to Supabase.");
            return false;
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}

public class SupabaseCreatureDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("creature_type")]
    public string CreatureType { get; set; } = string.Empty;

    [JsonPropertyName("habitat")]
    public string Habitat { get; set; } = string.Empty;

    [JsonPropertyName("rarity")]
    public string Rarity { get; set; } = string.Empty;

    [JsonPropertyName("base_price")]
    public decimal BasePrice { get; set; }

    [JsonPropertyName("mutations")]
    public List<string>? Mutations { get; set; }

    [JsonPropertyName("emoji_art")]
    public string EmojiArt { get; set; } = "🎴";

    [JsonPropertyName("art_color_gradient")]
    public string ArtColorGradient { get; set; } = "from-plum-deep to-plum-secondary";

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("serial_number")]
    public string? SerialNumber { get; set; }

    [JsonPropertyName("is_featured_showcase")]
    public bool IsFeaturedShowcase { get; set; }

    [JsonPropertyName("custom_valuation")]
    public decimal? CustomValuation { get; set; }

    [JsonPropertyName("is_available_in_shop")]
    public bool IsAvailableInShop { get; set; } = true;

    public Creature ToDomainModel()
    {
        Enum.TryParse<RarityTier>(Rarity, true, out var parsedRarity);

        return new Creature
        {
            Id = Id,
            Name = Name,
            Title = Title,
            CreatureType = CreatureType,
            Habitat = Habitat,
            Rarity = parsedRarity,
            BasePrice = BasePrice,
            Mutations = Mutations ?? new List<string>(),
            EmojiArt = EmojiArt,
            ArtColorGradient = ArtColorGradient,
            Description = Description,
            SerialNumber = SerialNumber,
            IsFeaturedShowcase = IsFeaturedShowcase,
            CustomValuation = CustomValuation,
            IsAvailableInShop = IsAvailableInShop
        };
    }
}

