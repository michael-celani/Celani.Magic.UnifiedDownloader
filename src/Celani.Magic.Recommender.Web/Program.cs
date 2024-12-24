using Celani.Magic.Downloader.Storage;
using Celani.Magic.ML;
using Celani.Magic.Recommender.Web.Components;
using Microsoft.Extensions.ML;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMagicML(builder.Configuration);
builder.Services.AddMagicStorage(builder.Configuration);
builder.Services.AddPredictionEnginePool<RecommenderCard, RecommenderCardPrediction>().FromFile(
    builder.Configuration.GetRequiredSection("Storage:Models:RecommendationPath").Value
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
