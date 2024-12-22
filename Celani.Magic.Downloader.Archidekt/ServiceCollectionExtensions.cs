using Celani.Magic.Downloader.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Celani.Magic.Downloader.Archidekt;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddArchidekt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddArchidektApi(configuration);
        services.AddSingleton<IMagicDownloader, ArchidektMagicDownloader>();
        return services;
    }

    private static IServiceCollection AddArchidektApi(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(ArchidektOptions.Options);
        services.AddOptionsWithValidateOnStart<ArchidektOptions>().Bind(options).ValidateDataAnnotations();

        // Add the Moxfield API client.
        services.AddRefitClient<IArchidektApi>().ConfigureHttpClient(
            (serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<ArchidektOptions>>().Value;

                client.BaseAddress = config.ArchidektApiUri;
            }
        );

        return services;
    }
}
