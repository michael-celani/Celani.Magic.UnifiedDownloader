using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Celani.Magic.ML;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMagicML(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(MLOptions.Options);
        services.AddOptionsWithValidateOnStart<MLOptions>().Bind(options).ValidateDataAnnotations();

        return services;
    }
}
