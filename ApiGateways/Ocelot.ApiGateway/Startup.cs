using Common.Logging.Correlation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        //var authScheme = "EShoppingGatewayAuthScheme";
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        // .AddJwtBearer(authScheme, options =>
        // {
        //     options.Authority = "https://localhost:9009";
        //     options.Audience = "EShoppingGateway";
        // });
             .AddJwtBearer(options =>
             {
                 options.Authority = "https://localhost:9009";
                 options.Audience = "EShoppingGateway";
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
