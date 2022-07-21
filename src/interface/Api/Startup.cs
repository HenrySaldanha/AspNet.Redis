using DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Api;
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

        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.RegisterRedis(Configuration);
        services.RegisterRepositories();
        services.RegisterServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseApiVersioning();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "v1"); });

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