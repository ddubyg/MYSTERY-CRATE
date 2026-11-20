-- ========================================================
-- MYSTERY CRATE: VEILBOUND: ORIGINS
-- Supabase PostgreSQL Database Schema & Initial Seed
-- ========================================================

-- 1. Enable UUID Extension if not already enabled
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- 2. Clean Existing Tables (if re-running)
DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS crate_opening_history CASCADE;
DROP TABLE IF EXISTS trade_history CASCADE;
DROP TABLE IF EXISTS marketplace_listings CASCADE;
DROP TABLE IF EXISTS vault_cards CASCADE;
DROP TABLE IF EXISTS mystery_packs CASCADE;
DROP TABLE IF EXISTS creatures CASCADE;

-- ========================================================
-- 3. Table Definitions
-- ========================================================

-- Creatures Catalog
CREATE TABLE creatures (
    id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    title TEXT NOT NULL,
    creature_type TEXT NOT NULL,
    habitat TEXT NOT NULL,
    rarity TEXT NOT NULL,
    base_price NUMERIC(12, 2) NOT NULL DEFAULT 10.00,
    mutations TEXT[] NOT NULL DEFAULT '{}',
    emoji_art TEXT NOT NULL DEFAULT '🎴',
    art_color_gradient TEXT NOT NULL DEFAULT 'from-plum-deep to-plum-secondary',
    description TEXT NOT NULL DEFAULT '',
    serial_number TEXT,
    is_featured_showcase BOOLEAN NOT NULL DEFAULT FALSE,
    custom_valuation NUMERIC(14, 2),
    is_available_in_shop BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Mystery Packs
CREATE TABLE mystery_packs (
    id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    subtitle TEXT NOT NULL,
    price NUMERIC(12, 2) NOT NULL,
    description TEXT NOT NULL,
    icon_emoji TEXT NOT NULL DEFAULT '📦',
    badge_text TEXT NOT NULL DEFAULT 'Standard',
    badge_color TEXT NOT NULL DEFAULT 'bg-purple-light text-purple-muted',
    box_gradient TEXT NOT NULL DEFAULT 'from-plum-deep via-plum-secondary to-purple-muted',
    odds_description TEXT NOT NULL DEFAULT '',
    min_guaranteed_rarity TEXT NOT NULL DEFAULT 'Common',
    card_count INTEGER NOT NULL DEFAULT 5,
    drop_table_json JSONB NOT NULL DEFAULT '{}'::jsonb,
    creature_pool_ids TEXT[] NOT NULL DEFAULT '{}',
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- User Vault (Owned Cards)
CREATE TABLE vault_cards (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    creature_id TEXT NOT NULL REFERENCES creatures(id) ON DELETE CASCADE,
    quantity INTEGER NOT NULL DEFAULT 1,
    acquired_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT uq_creature_owner UNIQUE (creature_id)
);

-- Marketplace Listings (The Exchange)
CREATE TABLE marketplace_listings (
    id TEXT PRIMARY KEY,
    creature_id TEXT NOT NULL REFERENCES creatures(id) ON DELETE CASCADE,
    seller_name TEXT NOT NULL,
    seller_badge TEXT NOT NULL DEFAULT 'Verified Trader',
    asking_price NUMERIC(12, 2) NOT NULL,
    listed_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    notes TEXT NOT NULL DEFAULT '',
    is_sold BOOLEAN NOT NULL DEFAULT FALSE
);

-- 5-Card Crate Opening History
CREATE TABLE crate_opening_history (
    id TEXT PRIMARY KEY,
    pack_name TEXT NOT NULL,
    crate_cost NUMERIC(12, 2) NOT NULL,
    card_count INTEGER NOT NULL DEFAULT 5,
    total_value NUMERIC(14, 2) NOT NULL,
    net_difference NUMERIC(14, 2) NOT NULL,
    outcome TEXT NOT NULL,
    timestamp TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    cards_summary JSONB NOT NULL DEFAULT '[]'::jsonb
);

-- Orders (Simulated Checkout)
CREATE TABLE orders (
    id TEXT PRIMARY KEY,
    customer_name TEXT NOT NULL,
    email TEXT NOT NULL,
    shipping_address TEXT NOT NULL,
    city TEXT NOT NULL,
    postal_code TEXT NOT NULL,
    subtotal NUMERIC(14, 2) NOT NULL,
    shipping_fee NUMERIC(12, 2) NOT NULL DEFAULT 0,
    total NUMERIC(14, 2) NOT NULL,
    simulated_payment_note TEXT NOT NULL,
    items_json JSONB NOT NULL DEFAULT '[]'::jsonb,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Trade History
CREATE TABLE trade_history (
    id TEXT PRIMARY KEY,
    partner_name TEXT NOT NULL,
    summary_text TEXT NOT NULL,
    completed_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- ========================================================
-- 4. Enable Row Level Security (RLS) & Public Access Policies
-- (Allows reading and demo writing via Supabase Anon Key)
-- ========================================================

ALTER TABLE creatures ENABLE ROW LEVEL SECURITY;
ALTER TABLE mystery_packs ENABLE ROW LEVEL SECURITY;
ALTER TABLE vault_cards ENABLE ROW LEVEL SECURITY;
ALTER TABLE marketplace_listings ENABLE ROW LEVEL SECURITY;
ALTER TABLE crate_opening_history ENABLE ROW LEVEL SECURITY;
ALTER TABLE orders ENABLE ROW LEVEL SECURITY;
ALTER TABLE trade_history ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Allow public read access to creatures" ON creatures FOR SELECT USING (true);
CREATE POLICY "Allow public read access to mystery_packs" ON mystery_packs FOR SELECT USING (true);
CREATE POLICY "Allow public all access to vault_cards" ON vault_cards FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Allow public all access to marketplace_listings" ON marketplace_listings FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Allow public all access to crate_opening_history" ON crate_opening_history FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Allow public all access to orders" ON orders FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Allow public all access to trade_history" ON trade_history FOR ALL USING (true) WITH CHECK (true);

-- ========================================================
-- 5. Seed Initial Data
-- ========================================================

-- Insert Creatures
INSERT INTO creatures (id, name, title, creature_type, habitat, rarity, base_price, mutations, emoji_art, art_color_gradient, description, serial_number, is_featured_showcase, custom_valuation, is_available_in_shop)
VALUES
('aurex-001', 'Aurex', 'The Prismatic Scarab', 'Insect / Celestial', 'Celestial Spires', 'Legendary', 5000.00, ARRAY['Gold', 'Rainbow'], '🪲', 'from-amber-950 via-purple-950 to-indigo-950', 'A legendary one-of-one specimen unearthed from the Astral Vaults of Aethelgard. Its hardened chitin refracts pure sunlight into prismatic arcs, whispered to bend temporal fate.', '001 / 001', TRUE, 5000000.00, FALSE),
('gloomspore-002', 'Gloomspore Hopper', 'Spore Tender of the Moss', 'Beast / Fungal', 'Whispering Woods', 'Common', 10.00, ARRAY['Shiny'], '🐸', 'from-emerald-950 via-teal-900 to-stone-900', 'A nimble amphibian whose glowing fungal carapace illuminates damp subterranean hollows.', NULL, FALSE, NULL, TRUE),
('tidepool-003', 'Tidepool Nymph', 'Weaver of Pearl Mists', 'Aquatic / Mystic', 'Sunken Sanctum', 'Common', 10.00, ARRAY['Glimmer'], '🧜‍♀️', 'from-cyan-950 via-sky-900 to-indigo-950', 'Dancing upon coral ridges, her translucent scales chime softly when the tide rushes inland.', NULL, FALSE, NULL, TRUE),
('cinder-fox-004', 'Cinder Fox', 'Ember of the Ashlands', 'Beast / Pyro', 'Volcanic Crag', 'Uncommon', 50.00, ARRAY['Foil'], '🦊', 'from-orange-950 via-amber-900 to-rose-950', 'Leaves blazing pawprints across cooled basalt that smolder for days under the winter twilight.', NULL, FALSE, NULL, TRUE),
('zephyr-005', 'Zephyr Sprite', 'Gale Courier', 'Elemental / Wind', 'Celestial Spires', 'Uncommon', 50.00, ARRAY['Crystal'], '🧚', 'from-teal-950 via-emerald-900 to-sky-950', 'Born from atmospheric thermals above the spires, guiding migratory flocks through storm clouds.', NULL, FALSE, NULL, TRUE),
('obsidian-006', 'Obsidian Sentinel', 'Bulwark of the Caldera', 'Golem / Earth', 'Deep Mines', 'Rare', 150.00, ARRAY['Silver'], '🗿', 'from-zinc-950 via-stone-900 to-neutral-950', 'Carved from volcanic glass millennia ago to watch over sacred subterranean fault lines.', NULL, FALSE, NULL, TRUE),
('abyssal-007', 'Abyssal Leviathan', 'Dread of the Deep Trenches', 'Aquatic / Abyssal', 'Abyssal Depths', 'Rare', 150.00, ARRAY['Shadow'], '🐋', 'from-slate-950 via-indigo-950 to-neutral-950', 'Glides silently beneath tectonic plates, emitting infrasonic pulses that disorient surface vessels.', NULL, FALSE, NULL, TRUE),
('moonlit-008', 'Moonlit Stalker', 'Shadow of the Crescent', 'Beast / Lunar', 'Whispering Woods', 'Epic', 500.00, ARRAY['Silver', 'Neon'], '🐆', 'from-purple-950 via-violet-900 to-slate-950', 'Only visible under full moonlight. Its spectral coat absorbs starlight to camouflage its strike.', NULL, FALSE, NULL, TRUE),
('storm-009', 'Storm Sovereign', 'Thunderlord of the Vortex', 'Avian / Storm', 'Celestial Spires', 'Epic', 500.00, ARRAY['Neon'], '🦅', 'from-sky-950 via-blue-900 to-violet-950', 'Rides supersonic hurricane fronts, channeling ball lightning through its razor-sharp plumage.', NULL, FALSE, NULL, TRUE),
('void-010', 'Void Wyrm', 'Devourer of Starlight', 'Dragon / Astral', 'Astral Void', 'Mythic', 1500.00, ARRAY['Cosmic'], '🐉', 'from-purple-950 via-fuchsia-950 to-indigo-950', 'Slithers through tears in reality, leaving trails of incandescent comet dust across forgotten orbits.', '104 / 500', FALSE, NULL, TRUE),
('solar-011', 'Solar Archon', 'Emissary of the Dawning Sun', 'Celestial / Solar', 'Solar Citadel', 'Mythic', 1500.00, ARRAY['Gold'], '☀️', 'from-amber-950 via-yellow-900 to-orange-950', 'Wreathed in celestial solar flares. Its sacred warhorn can awaken dormant stars.', '045 / 500', FALSE, NULL, TRUE),
('chrono-012', 'Chrono Chimera', 'Guardian of Fractured Timelines', 'Beast / Chrono', 'Timeless Expanse', 'Legendary', 5000.00, ARRAY['Prismatic'], '⏳', 'from-indigo-950 via-purple-900 to-amber-950', 'Phases between past, present, and future within an eye blink. Highly prized by temporal scholars.', '014 / 250', FALSE, NULL, TRUE),
('aethelgard-013', 'Aethelgard Prime', 'Heart of the Origin Veil', 'Titan / Cosmic', 'Celestial Spires', 'Celestial', 25000.00, ARRAY['Divine', 'Cosmic'], '👑', 'from-violet-950 via-indigo-900 to-amber-900', 'The primordial cornerstone of the Veilbound realm. Its eyes shine with the genesis of twenty galaxies.', '003 / 050', FALSE, NULL, TRUE),
('dune-serpent-014', 'Dune Serpent', 'Mirage of the Sunken Wastes', 'Reptile / Sand', 'Sunken Sanctum', 'Common', 10.00, '{}', '🐍', 'from-amber-950 via-stone-900 to-yellow-950', 'Burrows through hot sand at extraordinary velocity, surfacing only under the shade of sandstone pillars.', NULL, FALSE, NULL, TRUE),
('frost-behemoth-015', 'Frost Behemoth', 'Glacier of the North Peak', 'Beast / Cryo', 'Celestial Spires', 'Rare', 150.00, ARRAY['Crystal'], '🐻‍❄️', 'from-sky-950 via-cyan-900 to-blue-950', 'An immense creature cloaked in permafrost sheets that never thaw, even when bathed in dragon fire.', NULL, FALSE, NULL, TRUE);

-- Insert Mystery Packs
INSERT INTO mystery_packs (id, name, subtitle, price, description, icon_emoji, badge_text, badge_color, box_gradient, odds_description, min_guaranteed_rarity, card_count, drop_table_json, creature_pool_ids)
VALUES
('pack-novice', 'Novice Spores Pack', 'Whispering Woods Discovery', 100.00, 'A starter mystery pouch containing enchanted flora and forest dwellers. Perfect for novice collectors embarking into the Veil.', '🌿', 'Beginner Tier', 'bg-emerald-100 text-emerald-900 border-emerald-300', 'from-emerald-950 via-teal-900 to-stone-900', '5 Cards • 78% Common • 18% Uncommon • 3.8% Rare • 0.2% Epic', 'Common', 5, '{"Common": 78.0, "Uncommon": 18.0, "Rare": 3.8, "Epic": 0.2}', ARRAY['gloomspore-002', 'tidepool-003', 'dune-serpent-014', 'cinder-fox-004', 'zephyr-005']),
('pack-arcane', 'Arcane Relic Pack', 'Forged in Volcanic Chambers', 500.00, 'Sealed with ancient alchemical runes. Contains sturdy guardians and mystical creatures imbued with elemental energy.', '🔮', 'Popular Pick', 'bg-sky-100 text-sky-900 border-sky-300', 'from-indigo-950 via-purple-900 to-slate-900', '5 Cards • 65% Common • 24% Uncommon • 8% Rare • 2.8% Epic • 0.2% Mythic', 'Uncommon', 5, '{"Common": 65.0, "Uncommon": 24.0, "Rare": 8.0, "Epic": 2.8, "Mythic": 0.2}', ARRAY['cinder-fox-004', 'zephyr-005', 'obsidian-006', 'abyssal-007', 'frost-behemoth-015', 'moonlit-008']),
('pack-astral', 'Astral Mythic Crate', 'Vortex of the Starry Expanse', 2500.00, 'Forged from fallen meteoric alloys. Emits a low harmonic hum that resonates with high-tier mythical creatures.', '✨', 'High Tier', 'bg-rose-100 text-rose-900 border-rose-300', 'from-purple-950 via-rose-950 to-indigo-950', '5 Cards • 52% Common • 28% Uncommon • 12% Rare • 5.5% Epic • 2% Mythic • 0.5% Legendary', 'Rare', 5, '{"Common": 52.0, "Uncommon": 28.0, "Rare": 12.0, "Epic": 5.5, "Mythic": 2.0, "Legendary": 0.5}', ARRAY['obsidian-006', 'abyssal-007', 'moonlit-008', 'storm-009', 'void-010', 'solar-011', 'chrono-012']),
('pack-celestial', 'Celestial Veil Vault', 'The Sovereign Emperor''s Cache', 10000.00, 'The apex ₱10,000 mystery container of the Veilbound realm. Bound by celestial warding sigils, housing titanic artifacts, cosmic divinities, and high-value mutations.', '👑', 'Prestige Tier', 'bg-amber-100 text-amber-950 border-amber-400 font-bold', 'from-amber-950 via-violet-950 to-purple-950', '5 Cards • 45% Common • 25% Uncommon • 16% Rare • 8% Epic • 4% Mythic • 1.7% Legendary • 0.3% Celestial', 'Epic', 5, '{"Common": 45.0, "Uncommon": 25.0, "Rare": 16.0, "Epic": 8.0, "Mythic": 4.0, "Legendary": 1.7, "Celestial": 0.3}', ARRAY['moonlit-008', 'storm-009', 'void-010', 'solar-011', 'chrono-012', 'aethelgard-013']);

-- Insert Starter Vault Cards (User Collection)
INSERT INTO vault_cards (creature_id, quantity)
VALUES
('gloomspore-002', 1),
('tidepool-003', 1),
('cinder-fox-004', 1),
('zephyr-005', 1),
('obsidian-006', 1);

-- Insert Default Marketplace Listings
INSERT INTO marketplace_listings (id, creature_id, seller_name, seller_badge, asking_price, notes)
VALUES
('list-101', 'obsidian-006', 'Kaelen Stoneforge', 'Master Miner', 275.00, 'Pristine Silver mutation luster. Never used in combat trials.'),
('list-102', 'cinder-fox-004', 'Sora Cloudweaver', 'Air Nomad', 85.00, 'Extra fiery foil coat. Looking to trade or sell quickly.'),
('list-103', 'moonlit-008', 'Elena the Archivist', 'Grand Curator', 1850.00, 'Dual mutation: Silver + Neon. Kept in velvet protective sleeve.'),
('list-104', 'abyssal-007', 'Baron Vane', 'Abyssal Diplomat', 360.00, 'Deep trench specimen with rare Shadow essence.'),
('list-105', 'solar-011', 'Lord Lucian', 'Solar Champion', 4200.00, 'Serialized 045/500 with Golden mutation aura. Certified authentic.'),
('list-106', 'gloomspore-002', 'Pip the Wanderer', 'Novice Scout', 14.00, 'Cute little hopper, glowing green under darkness.');

