using Celani.Magic.Downloader.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Celani.Magic.Downloader.Moxfield;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMoxfield(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMoxfieldApi(configuration);
        services.AddSingleton<IMagicDownloader, MoxfieldMagicDownloader>();
        return services;
    }

    private static IServiceCollection AddMoxfieldApi(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(MoxfieldOptions.Options);
        services.AddOptionsWithValidateOnStart<MoxfieldOptions>().Bind(options).ValidateDataAnnotations();

        // Add the Moxfield API client.
        services.AddRefitClient<IMoxfieldApi>().ConfigureHttpClient(
            (serviceProvider, client) => {
                var config = serviceProvider.GetRequiredService<IOptions<MoxfieldOptions>>().Value;

                client.BaseAddress = config.MoxfieldApiUri;
                client.DefaultRequestHeaders.UserAgent.ParseAdd(config.MoxfieldApiKey);
            }
        );

        return services;
    }
}
