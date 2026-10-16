namespace MysteryCrate.Models;

public enum RarityTier
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Mythic,
    Legendary,
    Celestial
}

public static class RarityHelper
{
    public static decimal GetBasePrice(RarityTier tier) => tier switch
    {
        RarityTier.Common => 10m,
        RarityTier.Uncommon => 50m,
        RarityTier.Rare => 150m,
        RarityTier.Epic => 500m,
        RarityTier.Mythic => 1500m,
        RarityTier.Legendary => 5000m,
        RarityTier.Celestial => 25000m,
        _ => 10m
    };

    public static string GetBadgeClasses(RarityTier tier) => tier switch
    {
        RarityTier.Common => "bg-stone-100 text-stone-700 border-stone-300",
        RarityTier.Uncommon => "bg-emerald-50 text-emerald-800 border-emerald-300",
        RarityTier.Rare => "bg-sky-50 text-sky-800 border-sky-300",
        RarityTier.Epic => "bg-purple-100 text-purple-900 border-purple-300",
        RarityTier.Mythic => "bg-rose-50 text-rose-800 border-rose-300",
        RarityTier.Legendary => "bg-amber-100 text-amber-900 border-amber-400 font-semibold shadow-sm",
        RarityTier.Celestial => "bg-gradient-to-r from-violet-100 via-sky-100 to-amber-100 text-violet-950 border-violet-400 font-bold shadow-sm",
        _ => "bg-stone-100 text-stone-700 border-stone-300"
    };

    public static string GetBorderAccent(RarityTier tier) => tier switch
    {
        RarityTier.Common => "border-stone-200",
        RarityTier.Uncommon => "border-emerald-300",
        RarityTier.Rare => "border-sky-300",
        RarityTier.Epic => "border-purple-300",
        RarityTier.Mythic => "border-rose-300",
        RarityTier.Legendary => "border-gold-accent ring-1 ring-gold-accent/40",
        RarityTier.Celestial => "border-violet-400 ring-2 ring-violet-400/50",
        _ => "border-border-subtle"
    };
}

