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

        //add this in dependency injection
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        }).AddSwaggerGenNewtonsoftSupport();

        //add this in dependency injection
        services.AddHealthChecks()
            .AddRedis(Configuration.GetConnectionString("Redis"), name: "redis");

        //add this in dependency injection
        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        services.AddRouting(options => options.LowercaseUrls = true);
        services.RegisterRedis(Configuration);
        services.RegisterOptions(Configuration);
        services.RegisterRepositories();
        services.RegisterServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseApiVersioning();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "v1"); });

        //add this in dependency injection
        app.UseHealthChecks("/health-check",
            new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonSerializer.Serialize(
                    new
                    {
                        currentTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                        statusApplication = report.Status.ToString(),
                        healthChecks = report.Entries.Select(c=> new
                        {
                            check = c.Key,
                            status = Enum.GetName(typeof(HealthStatus),c.Value.Status)
                        })
                    });

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });

        //add this in dependency injection
        app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        //add this in dependency injection
        app.UseHealthChecksUI(o => o.UIPath = "/monitor");


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