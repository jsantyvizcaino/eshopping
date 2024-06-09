using Common.Logging.Correlation;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.ApiGateway;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
        services.AddOcelot()
            .AddCacheManager(o => o.WithDictionaryHandle());
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.AddCorrelationIdMiddleware();
        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints => {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Ingreso desde Ocelot");
            });
        });

        await app.UseOcelot();
    }
}
