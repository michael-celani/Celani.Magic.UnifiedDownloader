using Celani.Magic.Downloader.Archidekt;
using Celani.Magic.Downloader.Moxfield;
using Celani.Magic.Downloader.Storage;
using Celani.Magic.Scryfall;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    var enumConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddMagicStorage(builder.Configuration);
builder.Services.AddScryfall(builder.Configuration);
builder.Services.AddArchidekt(builder.Configuration);
builder.Services.AddMoxfield(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbManager = app.Services.GetRequiredService<ScryfallDatabaseManager>();
await dbManager.UpdateDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
