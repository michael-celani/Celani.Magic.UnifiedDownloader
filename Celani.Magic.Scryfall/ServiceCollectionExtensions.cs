using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScryfall(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(ScryfallOptions.Options);
        services.AddOptionsWithValidateOnStart<ScryfallOptions>().Bind(options).ValidateDataAnnotations();
        services.AddHttpClient("BulkData");

        return services.AddScryfallApi().AddSingleton<ScryfallDatabaseManager>();
    }

    private static IServiceCollection AddScryfallApi(this IServiceCollection services)
    {
        var enumConverter = new JsonStringEnumConverter();
        var serializerOptions = new JsonSerializerOptions();
        serializerOptions.Converters.Add(enumConverter);

        var settings = new RefitSettings(new SystemTextJsonContentSerializer(serializerOptions));

        services.AddRefitClient<IScryfallApi>(settings).ConfigureHttpClient(
            (serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<ScryfallOptions>>().Value;

                client.BaseAddress = config.ScryfallApiUri;
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Celani/Downloader 1.0");
                client.DefaultRequestHeaders.Accept.ParseAdd(MediaTypeNames.Application.Json);
            }
        );

        return services;
    }
}
