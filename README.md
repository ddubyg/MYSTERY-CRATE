# MYSTERY CRATE — VEILBOUND: ORIGINS
### Midterm Full-Stack Web Application Prototype (Blazor & Tailwind CSS)

Welcome to **MYSTERY CRATE**, an interactive collectible creature-card ecommerce and marketplace platform featuring the fictional universe **VEILBOUND: ORIGINS**.

---

## 1. Project Overview & Identity

- **Brand:** MYSTERY CRATE
- **Universe:** VEILBOUND: ORIGINS
- **Tech Stack:** ASP.NET Core 8.0 Blazor Web App (Interactive Server), Tailwind CSS v3, C#
- **Signature Featured Specimen:** **Aurex, the Prismatic Scarab** (1 of 1 Legendary with Golden + Rainbow mutations; illustrative showcase valuation: ₱5,000,000)

The application lets users discover mystical creatures, browse cards with search and filters, open tiered mystery packs, manage an in-memory collection vault, inspect individual cards, list owned cards for sale on peer-to-peer exchange, barter via an interactive trade hub, and complete a simulated ecommerce checkout.

---

## 2. Visual Design System

The application strictly implements the required warm ivory and deep plum aesthetic palette with gold and purple accents:

| Token Name | Hex Code | Purpose |
|---|---|---|
| **Warm Ivory** | `#F7F5F1` | Primary body background (`bg-ivory`) |
| **Card White** | `#FFFEFA` | Container and card surface background (`bg-card`) |
| **Deep Plum** | `#211A2B` | Header, hero banners, high-contrast actions (`bg-plum-deep`) |
| **Secondary Plum**| `#30223B` | Interactive hover states, sub-elements |
| **Muted Purple** | `#76618E` | Subtitles, labels, metadata badges |
| **Light Lavender**| `#F3EDF6` | Card detail accents, soft button fills |
| **Gold Accent** | `#E7C46B` | Prestige accents, currency symbols, badges (`text-gold-accent`) |
| **Primary Ink** | `#211C29` | Headings, main typography |
| **Muted Ink** | `#797381` | Secondary descriptions, timestamps |
| **Subtle Border** | `#E9E4EC` | Borders and dividers |

### Typography
- **Headings & Creature Titles:** Playfair Display (`font-display`) via Google Fonts with Georgia fallbacks.
- **Body & Controls:** DM Sans (`font-sans`) via Google Fonts with system UI fallbacks.

---

## 3. The Eight Implemented Screens & Routes

| Screen | Route | Razor File | Description |
|---|---|---|---|
| **1. Discover / Home** | `/` | `Components/Pages/Home.razor` | Hero section featuring **Aurex** with Golden + Rainbow badges and ₱5,000,000 showcase valuation; Rarity hierarchy; Curated creatures grid; Mutation multipliers section. |
| **2. Card Shop** | `/shop` | `Components/Pages/Shop.razor` | Catalog browsing with live text search, rarity filters, mutation filters, valuation/name sorting, and empty state handling. |
| **3. Mystery Packs** | `/packs` | `Components/Pages/Packs.razor` | Tiered mystery packs (Novice, Arcane, Astral, Celestial), unboxing odds explanations, confirmation modal, and animated card reveal modal. |
| **4. My Vault** | `/vault` | `Components/Pages/Vault.razor` | Personal collection summary, total cards, cumulative portfolio valuation, rarity distribution breakdown, search, and owned inventory grid. |
| **5. Card Details** | `/cards/{id}` | `Components/Pages/CardDetails.razor` | Large holographic card showcase, specimen lore, native habitat, serial number, valuation formula breakdown, and cart action. |
| **6. The Exchange** | `/exchange` | `Components/Pages/Exchange.razor` | Secondary marketplace with active fictional listings, instant simulated buying, and modal to list cards from personal vault. |
| **7. Trade Hub** | `/trades` | `Components/Pages/Trades.razor` | Barter matrix with realm traders (Elena, Baron Vane, Sora), fair exchange evaluation, two-sided proposal, confirmation, and trade history archive. |
| **8. Cart & Checkout** | `/cart`<br>`/checkout` | `Components/Pages/Cart.razor`<br>`Components/Pages/Checkout.razor` | Cart line items with quantity steppers, free shipping threshold (₱1,500), checkout form with validation, order receipt, and direct vault synchronization. |

---

## 4. Architectural Structure

```text
midterm/
├── Components/
│   ├── App.razor                        # Root HTML document, font loading, styles
│   ├── Routes.razor                     # Client router with styled 404 NotFound view
│   ├── _Imports.razor                   # Common namespaces and component imports
│   ├── Cards/
│   │   ├── CreatureCard.razor           # Reusable card with foil sheen, badges, pricing, cart action
│   │   ├── MutationBadge.razor          # Trait badges (Shiny, Foil, Gold, Rainbow, etc.)
│   │   └── RarityBadge.razor            # Seven-tier rarity badges (Common to Celestial)
│   ├── Layout/
│   │   ├── MainLayout.razor             # Header + Body + Footer + Toast container
│   │   ├── SiteHeader.razor             # Sticky responsive navigation with live cart & vault badges
│   │   └── SiteFooter.razor             # Academic project disclaimer & lore links
│   ├── Pages/
│   │   ├── Home.razor                   # Screen 1: Discover & Aurex Showcase
│   │   ├── Shop.razor                   # Screen 2: Card Shop Catalog
│   │   ├── Packs.razor                  # Screen 3: Mystery Packs & Unboxing Reveal
│   │   ├── Vault.razor                  # Screen 4: My Vault / Portfolio
│   │   ├── CardDetails.razor            # Screen 5: Specimen Details & Formula
│   │   ├── Exchange.razor               # Screen 6: Secondary Marketplace & Player Listings
│   │   ├── Trades.razor                 # Screen 7: Barter Exchange Hub
│   │   ├── Cart.razor                   # Screen 8a: Shopping Cart
│   │   ├── Checkout.razor               # Screen 8b: Form Validation & Order Success
│   │   └── Error.razor                  # Themed error boundary
│   └── Shared/
│       ├── ConfirmationDialog.razor     # Reusable confirmation modal
│       ├── EmptyState.razor             # Reusable empty/not-found state
│       ├── FilterBar.razor              # Reusable filter and sort bar
│       ├── LoadingState.razor           # Arcane loading spinner
│       ├── NotificationToast.razor      # Reactive feedback toast notification
│       ├── PriceDisplay.razor           # Standardized currency formatting (₱)
│       └── SearchBar.razor              # Real-time search input with clear button
├── Data/
│   └── SampleData.cs                    # Centralized seed data: creatures, packs, listings, traders
├── Models/
│   ├── CartItem.cs                      # Cart items and types
│   ├── Creature.cs                      # Card entity and deterministic valuation method
│   ├── Listing.cs                       # Marketplace listing model
│   ├── Mutation.cs                      # Mutation trait definitions & multipliers
│   ├── MysteryPack.cs                   # Pack definition & drop pools
│   ├── Order.cs                         # Simulated order model
│   ├── Rarity.cs                        # Rarity tiers and base prices
│   └── TradeOffer.cs                    # Trade partners, proposals, and history records
├── Properties/
│   └── launchSettings.json              # Development ports (http: 5282, https: 7153)
├── Services/
│   ├── CartService.cs                   # Scoped cart state, reactive updates, item management
│   ├── CollectionService.cs             # Scoped vault inventory, valuation, rarity counts
│   ├── CreatureService.cs               # Creature catalog query, filter, and search logic
│   ├── MarketplaceService.cs            # Active listings, simulated purchases, seller posts
│   ├── MysteryPackService.cs            # Weighted unboxing simulation and vault injection
│   ├── NotificationService.cs           # Reactive toast feedback service
│   └── TradeService.cs                  # Trade partner inventory, validation, barter swaps
├── Styles/
│   └── input.css                        # Tailwind directives, Google Fonts, prismatic effects
├── wwwroot/
│   ├── app.css                          # Compiled Tailwind CSS distribution
│   └── favicon.png
├── MysteryCrate.csproj                  # .NET 8 Web SDK project with automated Tailwind build target
├── package.json                         # Node dependencies (tailwindcss v3.4.17)
├── Program.cs                           # DI registration of domain services and Interactive Server
├── README.md                            # Comprehensive project documentation
└── tailwind.config.js                   # Custom design tokens, fonts, and animation keyframes
```

---

## 5. How to Run the Application

### Option A: Running from Visual Studio
1. Open Visual Studio 2022 (v17.8+).
2. Click **Open a project or solution** and select `MysteryCrate.csproj` inside `c:\Users\L23X17W27\Desktop\midterm`.
3. Select the `http` launch profile (or press **F5** / **Ctrl+F5**).
4. The application will build, automatically compile Tailwind CSS via MSBuild target, and launch at:
   `http://localhost:5282`

### Option B: Running from Terminal (CLI)
1. Open PowerShell or Command Prompt in `c:\Users\L23X17W27\Desktop\midterm`:
   ```powershell
   dotnet run --launch-profile http
   ```
2. Open your browser and navigate to:
   `http://localhost:5282`

---

## 6. How Tailwind CSS Builds

Tailwind CSS is integrated via Node.js / npm with automated MSBuild compilation:
- **Source CSS:** `Styles/input.css`
- **Output CSS:** `wwwroot/app.css`
- **Configuration:** `tailwind.config.js`
- **Build Target:** `MysteryCrate.csproj` contains `<Target Name="BuildTailwind" BeforeTargets="Build">` which runs `npm run build:css` whenever the project is built.
- **Manual CSS Rebuild Command:**
  ```powershell
  npm run build:css
  ```
- **Watch Mode (Live Development):**
  ```powershell
  npm run watch:css
  ```

---

## 7. Valuation Rules & Pricing Formula

- **Common:** Base ₱10
- **Uncommon:** Base ₱50
- **Rare:** Base ₱150
- **Epic:** Base ₱500
- **Mythic:** Base ₱1,500
- **Legendary:** Base ₱5,000
- **Celestial:** Base ₱25,000

**Deterministic Valuation Calculation:**
$$\text{Valuation} = \text{Base Price} \times \prod (\text{Mutation Multipliers})$$

- Trait Multipliers:
  - Shiny: `1.25x` | Glimmer: `1.35x` | Foil: `1.50x` | Silver: `1.75x`
  - Crystal: `2.00x` | Neon: `2.00x` | Shadow: `2.20x` | Gold: `2.50x`
  - Rainbow: `3.00x` | Cosmic: `3.50x` | Prismatic: `4.00x` | Divine: `5.00x`
- **Aurex Signature Exception:** Aurex, the Prismatic Scarab features a fixed illustrative collector valuation of **₱5,000,000** accompanied by mandatory student demonstration disclaimers.

---

## 8. State Management & Lifecycle

- Services (`CartService`, `CollectionService`, `MysteryPackService`, `MarketplaceService`, `TradeService`, `NotificationService`) are registered as **Scoped** in `Program.cs`.
- Each browser circuit / user session has its own isolated, persistent state for the duration of the circuit session.
- Pack openings, marketplace purchases, barter trades, and checkout completions directly update the user's `CollectionService` vault in real time via C# event actions (`OnChange?.Invoke()`), ensuring zero full-page reloads.

---

## 9. Tested & Verified Features

| Verification Check | Status | Result |
|---|---|---|
| Project Clean Build | Passed | 0 Errors, 0 Warnings (`dotnet build`) |
| Tailwind CSS Compilation | Passed | 38.4 KB stylesheet generated with all custom theme tokens |
| Discover Screen (`/`) | Passed | HTTP 200; Aurex showcase, ₱5,000,000 valuation, rarity spectrum |
| Card Shop (`/shop`) | Passed | HTTP 200; search, rarity/mutation filter, valuation sorting |
| Mystery Packs (`/packs`) | Passed | HTTP 200; tier boxes, odds, unboxing confirmation & reveal |
| My Vault (`/vault`) | Passed | HTTP 200; starter collection, portfolio sum, live counts |
| Card Details (`/cards/{id}`) | Passed | HTTP 200; holographic art, lore, formula, cart action |
| Missing Card Route | Passed | HTTP 200; elegant EmptyState fallback for nonexistent IDs |
| The Exchange (`/exchange`) | Passed | HTTP 200; seller listings, simulated buying, list-card modal |
| Trade Hub (`/trades`) | Passed | HTTP 200; 3 realm traders, 2-sided offer matrix, valuation check |
| Cart (`/cart`) | Passed | HTTP 200; quantity steppers, subtotal, free shipping calc |
| Checkout (`/checkout`) | Passed | HTTP 200; form validation, simulated payment, order receipt |
| Invalid Route (404) | Passed | HTTP 404; themed NotFound layout with link to Discover |

#   M Y S T E R Y - C R A T E  
 