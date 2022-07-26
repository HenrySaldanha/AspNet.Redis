using DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;

namespace Api;

[ExcludeFromCodeCoverage]
public class Startup
{
    public IConfiguration Configuration { get; }
    private static readonly string PROJECT_NAME = "AspNet.Redis";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        }).AddSwaggerGenNewtonsoftSupport();

        services.AddHealthChecks()
            .AddRedis(Configuration.GetConnectionString("Redis"), name: "redis");

        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("Redis");
            options.InstanceName = $"{PROJECT_NAME}:";
        });

        services.AddRouting(options => options.LowercaseUrls = true);
        services.RegisterOptions(Configuration);
        services.RegisterRepositories();
        services.RegisterServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseApiVersioning();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "v1"); });

        app.UseHealthChecks("/health-check",
            new HealthCheckOptions
            {
                ResponseWriter = async (context, healthReport) =>
                {
                    var result = JsonSerializer.Serialize(
                    new
                    {
                        time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                        status = healthReport.Status.ToString(),
                        healthChecks = healthReport.Entries.Select(c => new
                        {
                            check = c.Key,
                            status = Enum.GetName(typeof(HealthStatus), c.Value.Status)
                        })
                    });

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });

        app.UseHealthChecks("/health-checks-with-response", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseHealthChecksUI(o => o.UIPath = "/status");

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}