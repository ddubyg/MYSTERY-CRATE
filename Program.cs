using MysteryCrate.Components;
using MysteryCrate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register application domain services as Scoped per circuit session
builder.Services.AddScoped<CreatureService>();
builder.Services.AddScoped<CollectionService>();
builder.Services.AddScoped<MysteryPackService>();
builder.Services.AddScoped<MarketplaceService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
