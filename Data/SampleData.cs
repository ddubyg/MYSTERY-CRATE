namespace MysteryCrate.Data;

using MysteryCrate.Models;

public static class SampleData
{
    public static List<Creature> GetAllCreatures()
    {
        return new List<Creature>
        {
            new Creature
            {
                Id = "aurex-001",
                Name = "Aurex",
                Title = "The Prismatic Scarab",
                CreatureType = "Insect / Celestial",
                Habitat = "Celestial Spires",
                Rarity = RarityTier.Legendary,
                BasePrice = 5000m,
                Mutations = new List<string> { "Gold", "Rainbow" },
                EmojiArt = "🪲",
                ArtColorGradient = "from-amber-950 via-purple-950 to-indigo-950",
                Description = "A legendary one-of-one specimen unearthed from the Astral Vaults of Aethelgard. Its hardened chitin refracts pure sunlight into prismatic arcs, whispered to bend temporal fate.",
                SerialNumber = "001 / 001",
                IsFeaturedShowcase = true,
                CustomValuation = 5000000m, // Illustrative collector valuation: ₱5,000,000
                IsAvailableInShop = false // Unique showcase, cannot simply buy standard copy from shop
            },
            new Creature
            {
                Id = "gloomspore-002",
                Name = "Gloomspore Hopper",
                Title = "Spore Tender of the Moss",
                CreatureType = "Beast / Fungal",
                Habitat = "Whispering Woods",
                Rarity = RarityTier.Common,
                BasePrice = 10m,
                Mutations = new List<string> { "Shiny" },
                EmojiArt = "🐸",
                ArtColorGradient = "from-emerald-950 via-teal-900 to-stone-900",
                Description = "A nimble amphibian whose glowing fungal carapace illuminates damp subterranean hollows."
            },
            new Creature
            {
                Id = "tidepool-003",
                Name = "Tidepool Nymph",
                Title = "Weaver of Pearl Mists",
                CreatureType = "Aquatic / Mystic",
                Habitat = "Sunken Sanctum",
                Rarity = RarityTier.Common,
                BasePrice = 10m,
                Mutations = new List<string> { "Glimmer" },
                EmojiArt = "🧜‍♀️",
                ArtColorGradient = "from-cyan-950 via-sky-900 to-indigo-950",
                Description = "Dancing upon coral ridges, her translucent scales chime softly when the tide rushes inland."
            },
            new Creature
            {
                Id = "cinder-fox-004",
                Name = "Cinder Fox",
                Title = "Ember of the Ashlands",
                CreatureType = "Beast / Pyro",
                Habitat = "Volcanic Crag",
                Rarity = RarityTier.Uncommon,
                BasePrice = 50m,
                Mutations = new List<string> { "Foil" },
                EmojiArt = "🦊",
                ArtColorGradient = "from-orange-950 via-amber-900 to-rose-950",
                Description = "Leaves blazing pawprints across cooled basalt that smolder for days under the winter twilight."
            },
            new Creature
            {
                Id = "zephyr-005",
                Name = "Zephyr Sprite",
                Title = "Gale Courier",
                CreatureType = "Elemental / Wind",
                Habitat = "Celestial Spires",
                Rarity = RarityTier.Uncommon,
                BasePrice = 50m,
                Mutations = new List<string> { "Crystal" },
                EmojiArt = "🧚",
                ArtColorGradient = "from-teal-950 via-emerald-900 to-sky-950",
                Description = "Born from atmospheric thermals above the spires, guiding migratory flocks through storm clouds."
            },
            new Creature
            {
                Id = "obsidian-006",
                Name = "Obsidian Sentinel",
                Title = "Bulwark of the Caldera",
                CreatureType = "Golem / Earth",
                Habitat = "Deep Mines",
                Rarity = RarityTier.Rare,
                BasePrice = 150m,
                Mutations = new List<string> { "Silver" },
                EmojiArt = "🗿",
                ArtColorGradient = "from-zinc-950 via-stone-900 to-neutral-950",
                Description = "Carved from volcanic glass millennia ago to watch over sacred subterranean fault lines."
            },
            new Creature
            {
                Id = "abyssal-007",
                Name = "Abyssal Leviathan",
                Title = "Dread of the Deep Trenches",
                CreatureType = "Aquatic / Abyssal",
                Habitat = "Abyssal Depths",
                Rarity = RarityTier.Rare,
                BasePrice = 150m,
                Mutations = new List<string> { "Shadow" },
                EmojiArt = "🐋",
                ArtColorGradient = "from-slate-950 via-indigo-950 to-neutral-950",
                Description = "Glides silently beneath tectonic plates, emitting infrasonic pulses that disorient surface vessels."
            },
            new Creature
            {
                Id = "moonlit-008",
                Name = "Moonlit Stalker",
                Title = "Shadow of the Crescent",
                CreatureType = "Beast / Lunar",
                Habitat = "Whispering Woods",
                Rarity = RarityTier.Epic,
                BasePrice = 500m,
                Mutations = new List<string> { "Silver", "Neon" },
                EmojiArt = "🐆",
                ArtColorGradient = "from-purple-950 via-violet-900 to-slate-950",
                Description = "Only visible under full moonlight. Its spectral coat absorbs starlight to camouflage its strike."
            },
            new Creature
            {
                Id = "storm-009",
                Name = "Storm Sovereign",
                Title = "Thunderlord of the Vortex",
                CreatureType = "Avian / Storm",
                Habitat = "Celestial Spires",
                Rarity = RarityTier.Epic,
                BasePrice = 500m,
                Mutations = new List<string> { "Neon" },
                EmojiArt = "🦅",
                ArtColorGradient = "from-sky-950 via-blue-900 to-violet-950",
                Description = "Rides supersonic hurricane fronts, channeling ball lightning through its razor-sharp plumage."
            },
            new Creature
            {
                Id = "void-010",
                Name = "Void Wyrm",
                Title = "Devourer of Starlight",
                CreatureType = "Dragon / Astral",
                Habitat = "Astral Void",
                Rarity = RarityTier.Mythic,
                BasePrice = 1500m,
                Mutations = new List<string> { "Cosmic" },
                EmojiArt = "🐉",
                ArtColorGradient = "from-purple-950 via-fuchsia-950 to-indigo-950",
                Description = "Slithers through tears in reality, leaving trails of incandescent comet dust across forgotten orbits.",
                SerialNumber = "104 / 500"
            },
            new Creature
            {
                Id = "solar-011",
                Name = "Solar Archon",
                Title = "Emissary of the Dawning Sun",
                CreatureType = "Celestial / Solar",
                Habitat = "Solar Citadel",
                Rarity = RarityTier.Mythic,
                BasePrice = 1500m,
                Mutations = new List<string> { "Gold" },
                EmojiArt = "☀️",
                ArtColorGradient = "from-amber-950 via-yellow-900 to-orange-950",
                Description = "Wreathed in celestial solar flares. Its sacred warhorn can awaken dormant stars.",
                SerialNumber = "045 / 500"
            },
            new Creature
            {
                Id = "chrono-012",
                Name = "Chrono Chimera",
                Title = "Guardian of Fractured Timelines",
                CreatureType = "Beast / Chrono",
                Habitat = "Timeless Expanse",
                Rarity = RarityTier.Legendary,
                BasePrice = 5000m,
                Mutations = new List<string> { "Prismatic" },
                EmojiArt = "⏳",
                ArtColorGradient = "from-indigo-950 via-purple-900 to-amber-950",
                Description = "Phases between past, present, and future within an eye blink. Highly prized by temporal scholars.",
                SerialNumber = "014 / 250"
            },
            new Creature
            {
                Id = "aethelgard-013",
                Name = "Aethelgard Prime",
                Title = "Heart of the Origin Veil",
                CreatureType = "Titan / Cosmic",
                Habitat = "Celestial Spires",
                Rarity = RarityTier.Celestial,
                BasePrice = 25000m,
                Mutations = new List<string> { "Divine", "Cosmic" },
                EmojiArt = "👑",
                ArtColorGradient = "from-violet-950 via-indigo-900 to-amber-900",
                Description = "The primordial cornerstone of the Veilbound realm. Its eyes shine with the genesis of twenty galaxies.",
                SerialNumber = "003 / 050"
            },
            new Creature
            {
                Id = "dune-serpent-014",
                Name = "Dune Serpent",
                Title = "Mirage of the Sunken Wastes",
                CreatureType = "Reptile / Sand",
                Habitat = "Sunken Sanctum",
                Rarity = RarityTier.Common,
                BasePrice = 10m,
                Mutations = new List<string>(),
                EmojiArt = "🐍",
                ArtColorGradient = "from-amber-950 via-stone-900 to-yellow-950",
                Description = "Burrows through hot sand at extraordinary velocity, surfacing only under the shade of sandstone pillars."
            },
            new Creature
            {
                Id = "frost-behemoth-015",
                Name = "Frost Behemoth",
                Title = "Glacier of the North Peak",
                CreatureType = "Beast / Cryo",
                Habitat = "Celestial Spires",
                Rarity = RarityTier.Rare,
                BasePrice = 150m,
                Mutations = new List<string> { "Crystal" },
                EmojiArt = "🐻‍❄️",
                ArtColorGradient = "from-sky-950 via-cyan-900 to-blue-950",
                Description = "An immense creature cloaked in permafrost sheets that never thaw, even when bathed in dragon fire."
            }
        };
    }

    public static List<MysteryPack> GetDefaultMysteryPacks()
    {
        return new List<MysteryPack>
        {
            new MysteryPack
            {
                Id = "pack-novice",
                Name = "Novice Spores Pack",
                Subtitle = "Whispering Woods Discovery",
                Price = 100m,
                Description = "A starter mystery pouch containing enchanted flora and forest dwellers. Perfect for novice collectors embarking into the Veil.",
                IconEmoji = "🌿",
                BadgeText = "Beginner Tier",
                BadgeColor = "bg-emerald-100 text-emerald-900 border-emerald-300",
                BoxGradient = "from-emerald-950 via-teal-900 to-stone-900",
                OddsDescription = "5 Cards • 78% Common • 18% Uncommon • 3.8% Rare • 0.2% Epic",
                MinGuaranteedRarity = RarityTier.Common,
                CardCount = 5,
                DropTable = new CrateDropTable
                {
                    Weights = new Dictionary<RarityTier, double>
                    {
                        { RarityTier.Common, 78.0 },
                        { RarityTier.Uncommon, 18.0 },
                        { RarityTier.Rare, 3.8 },
                        { RarityTier.Epic, 0.2 }
                    },
                    MutationChance = 0.20
                },
                CreaturePoolIds = new List<string> { "gloomspore-002", "tidepool-003", "dune-serpent-014", "cinder-fox-004", "zephyr-005" }
            },
            new MysteryPack
            {
                Id = "pack-arcane",
                Name = "Arcane Relic Pack",
                Subtitle = "Forged in Volcanic Chambers",
                Price = 500m,
                Description = "Sealed with ancient alchemical runes. Contains sturdy guardians and mystical creatures imbued with elemental energy.",
                IconEmoji = "🔮",
                BadgeText = "Popular Pick",
                BadgeColor = "bg-sky-100 text-sky-900 border-sky-300",
                BoxGradient = "from-indigo-950 via-purple-900 to-slate-900",
                OddsDescription = "5 Cards • 65% Common • 24% Uncommon • 8% Rare • 2.8% Epic • 0.2% Mythic",
                MinGuaranteedRarity = RarityTier.Uncommon,
                CardCount = 5,
                DropTable = new CrateDropTable
                {
                    Weights = new Dictionary<RarityTier, double>
                    {
                        { RarityTier.Common, 65.0 },
                        { RarityTier.Uncommon, 24.0 },
                        { RarityTier.Rare, 8.0 },
                        { RarityTier.Epic, 2.8 },
                        { RarityTier.Mythic, 0.2 }
                    },
                    MutationChance = 0.28
                },
                CreaturePoolIds = new List<string> { "cinder-fox-004", "zephyr-005", "obsidian-006", "abyssal-007", "frost-behemoth-015", "moonlit-008" }
            },
            new MysteryPack
            {
                Id = "pack-astral",
                Name = "Astral Mythic Crate",
                Subtitle = "Vortex of the Starry Expanse",
                Price = 2500m,
                Description = "Forged from fallen meteoric alloys. Emits a low harmonic hum that resonates with high-tier mythical creatures.",
                IconEmoji = "✨",
                BadgeText = "High Tier",
                BadgeColor = "bg-rose-100 text-rose-900 border-rose-300",
                BoxGradient = "from-purple-950 via-rose-950 to-indigo-950",
                OddsDescription = "5 Cards • 52% Common • 28% Uncommon • 12% Rare • 5.5% Epic • 2% Mythic • 0.5% Legendary",
                MinGuaranteedRarity = RarityTier.Rare,
                CardCount = 5,
                DropTable = new CrateDropTable
                {
                    Weights = new Dictionary<RarityTier, double>
                    {
                        { RarityTier.Common, 52.0 },
                        { RarityTier.Uncommon, 28.0 },
                        { RarityTier.Rare, 12.0 },
                        { RarityTier.Epic, 5.5 },
                        { RarityTier.Mythic, 2.0 },
                        { RarityTier.Legendary, 0.5 }
                    },
                    MutationChance = 0.35
                },
                CreaturePoolIds = new List<string> { "obsidian-006", "abyssal-007", "moonlit-008", "storm-009", "void-010", "solar-011", "chrono-012" }
            },
            new MysteryPack
            {
                Id = "pack-celestial",
                Name = "Celestial Veil Vault",
                Subtitle = "The Sovereign Emperor's Cache",
                Price = 10000m,
                Description = "The apex ₱10,000 mystery container of the Veilbound realm. Bound by celestial warding sigils, housing titanic artifacts, cosmic divinities, and high-value mutations.",
                IconEmoji = "👑",
                BadgeText = "Prestige Tier",
                BadgeColor = "bg-amber-100 text-amber-950 border-amber-400 font-bold",
                BoxGradient = "from-amber-950 via-violet-950 to-purple-950",
                OddsDescription = "5 Cards • 45% Common • 25% Uncommon • 16% Rare • 8% Epic • 4% Mythic • 1.7% Legendary • 0.3% Celestial",
                MinGuaranteedRarity = RarityTier.Epic,
                CardCount = 5,
                DropTable = new CrateDropTable
                {
                    Weights = new Dictionary<RarityTier, double>
                    {
                        { RarityTier.Common, 45.0 },
                        { RarityTier.Uncommon, 25.0 },
                        { RarityTier.Rare, 16.0 },
                        { RarityTier.Epic, 8.0 },
                        { RarityTier.Mythic, 4.0 },
                        { RarityTier.Legendary, 1.7 },
                        { RarityTier.Celestial, 0.3 }
                    },
                    MutationChance = 0.45
                },
                CreaturePoolIds = new List<string> { "moonlit-008", "storm-009", "void-010", "solar-011", "chrono-012", "aethelgard-013" }
            }
        };
    }

    public static List<Listing> GetDefaultListings(List<Creature> allCreatures)
    {
        var creatureMap = allCreatures.ToDictionary(c => c.Id);

        return new List<Listing>
        {
            new Listing
            {
                Id = "list-101",
                CreatureId = "obsidian-006",
                Creature = creatureMap["obsidian-006"],
                SellerName = "Kaelen Stoneforge",
                SellerBadge = "Master Miner",
                AskingPrice = 275m,
                Notes = "Pristine Silver mutation luster. Never used in combat trials."
            },
            new Listing
            {
                Id = "list-102",
                CreatureId = "cinder-fox-004",
                Creature = creatureMap["cinder-fox-004"],
                SellerName = "Sora Cloudweaver",
                SellerBadge = "Air Nomad",
                AskingPrice = 85m,
                Notes = "Extra fiery foil coat. Looking to trade or sell quickly."
            },
            new Listing
            {
                Id = "list-103",
                CreatureId = "moonlit-008",
                Creature = creatureMap["moonlit-008"],
                SellerName = "Elena the Archivist",
                SellerBadge = "Grand Curator",
                AskingPrice = 1850m,
                Notes = "Dual mutation: Silver + Neon. Kept in velvet protective sleeve."
            },
            new Listing
            {
                Id = "list-104",
                CreatureId = "abyssal-007",
                Creature = creatureMap["abyssal-007"],
                SellerName = "Baron Vane",
                SellerBadge = "Abyssal Diplomat",
                AskingPrice = 360m,
                Notes = "Deep trench specimen with rare Shadow essence."
            },
            new Listing
            {
                Id = "list-105",
                CreatureId = "solar-011",
                Creature = creatureMap["solar-011"],
                SellerName = "Lord Lucian",
                SellerBadge = "Solar Champion",
                AskingPrice = 4200m,
                Notes = "Serialized 045/500 with Golden mutation aura. Certified authentic."
            },
            new Listing
            {
                Id = "list-106",
                CreatureId = "gloomspore-002",
                Creature = creatureMap["gloomspore-002"],
                SellerName = "Pip the Wanderer",
                SellerBadge = "Novice Scout",
                AskingPrice = 14m,
                Notes = "Cute little hopper, glowing green under darkness."
            }
        };
    }

    public static List<TradePartner> GetDefaultTradePartners()
    {
        return new List<TradePartner>
        {
            new TradePartner
            {
                Id = "partner-elena",
                Name = "Elena the Archivist",
                Title = "Grand Curator of the Spire Vaults",
                AvatarEmoji = "🧙‍♀️",
                Bio = "Scholarly collector preserving ancient records and rare elemental specimens. Fair and knowledgeable trade partner.",
                InventoryCreatureIds = new List<string> { "storm-009", "zephyr-005", "tidepool-003", "chrono-012" }
            },
            new TradePartner
            {
                Id = "partner-vane",
                Name = "Baron Vane",
                Title = "Undersea Merchant Lord",
                AvatarEmoji = "🔱",
                Bio = "Mysterious connoisseur of deep-sea trenches and shadowy entities. Always interested in obscure mutations.",
                InventoryCreatureIds = new List<string> { "abyssal-007", "obsidian-006", "void-010" }
            },
            new TradePartner
            {
                Id = "partner-sora",
                Name = "Sora Cloudweaver",
                Title = "Nomadic Aeromancer",
                AvatarEmoji = "🦅",
                Bio = "Roams the Celestial Spires in search of swift beasts and shimmering sky sprites.",
                InventoryCreatureIds = new List<string> { "cinder-fox-004", "frost-behemoth-015", "gloomspore-002", "solar-011" }
            }
        };
    }
}

