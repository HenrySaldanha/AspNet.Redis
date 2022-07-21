using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Cache;
using Repository.Cache.Redis;

namespace DependencyInjection;

public static class DependencyInjection
{
    private static readonly string PROJECT_NAME = "AspNet.Redis";

    public static void RegisterServices(this IServiceCollection services)
    {
      //  services.AddScoped<IService, Service>();
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ICacheRepository, DistributedCacheRedis>();
    }

    public static void RegisterRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICacheRepository, DistributedCacheRedis>();
    }

    public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = $"{PROJECT_NAME}:";
        });
    }
}