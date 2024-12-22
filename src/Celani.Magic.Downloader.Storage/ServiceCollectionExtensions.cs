using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMagicStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(StorageOptions.Options);
        services.AddOptionsWithValidateOnStart<StorageOptions>().Bind(options).ValidateDataAnnotations();

        services.AddDbContextFactory<MagicContext>((serviceProvider, options) =>
        {
            var dataOptions = serviceProvider.GetRequiredService<IOptions<StorageOptions>>().Value;
            options.UseSqlite($"Data Source={dataOptions.DbPath};foreign keys=true");
        });

        return services;
    }
}
