namespace MysteryCrate.Models;

public static class MutationHelper
{
    public static readonly string[] AllMutations = new[]
    {
        "Shiny",
        "Foil",
        "Glimmer",
        "Silver",
        "Crystal",
        "Gold",
        "Shadow",
        "Rainbow",
        "Cosmic",
        "Neon",
        "Prismatic",
        "Divine"
    };

    public static decimal GetMultiplier(string mutation) => mutation.Trim().ToLowerInvariant() switch
    {
        "shiny" => 1.25m,
        "glimmer" => 1.35m,
        "foil" => 1.50m,
        "silver" => 1.75m,
        "crystal" => 2.00m,
        "neon" => 2.00m,
        "shadow" => 2.20m,
        "gold" => 2.50m,
        "rainbow" => 3.00m,
        "cosmic" => 3.50m,
        "prismatic" => 4.00m,
        "divine" => 5.00m,
        _ => 1.10m
    };

    public static string GetBadgeClasses(string mutation) => mutation.Trim().ToLowerInvariant() switch
    {
        "shiny" => "bg-cyan-50 text-cyan-800 border-cyan-300",
        "glimmer" => "bg-teal-50 text-teal-800 border-teal-300",
        "foil" => "bg-slate-100 text-slate-800 border-slate-300 font-medium",
        "silver" => "bg-gray-100 text-gray-800 border-gray-400 font-medium",
        "crystal" => "bg-blue-50 text-blue-800 border-blue-300",
        "neon" => "bg-lime-50 text-lime-800 border-lime-400 font-medium",
        "shadow" => "bg-zinc-800 text-zinc-100 border-zinc-600 font-medium",
        "gold" => "bg-amber-100 text-amber-900 border-amber-400 font-semibold",
        "rainbow" => "bg-gradient-to-r from-red-100 via-yellow-100 via-green-100 via-blue-100 to-purple-100 text-purple-900 border-purple-300 font-semibold",
        "cosmic" => "bg-indigo-900 text-indigo-100 border-indigo-500 font-medium",
        "prismatic" => "bg-gradient-to-r from-pink-100 via-purple-100 to-indigo-100 text-indigo-950 border-purple-400 font-semibold",
        "divine" => "bg-yellow-50 text-yellow-950 border-yellow-400 ring-1 ring-yellow-400/50 font-bold",
        _ => "bg-stone-100 text-stone-800 border-stone-300"
    };
}

