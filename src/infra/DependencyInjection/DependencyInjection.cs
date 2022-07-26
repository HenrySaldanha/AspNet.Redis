using Application.IServices;
using Application.Services;
using ExternalInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Cache;
using Repository.Cache.Redis;
using System.Diagnostics.CodeAnalysis;

namespace DependencyInjection;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFibonacciSequenceService, FibonacciSequenceService>();
        services.AddScoped<IIndicatorsService, IndicatorsService>();
        services.AddScoped<IGithubService, GithubService>();
        services.AddScoped<IGitHubApi, GitHubApi>();
        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ICacheRepository, DistributedCacheRedis>();
        return services;
    }

    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GitHubApiOptions>(configuration.GetSection("GitHubApi"));
        return services;
    }
}