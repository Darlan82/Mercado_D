using MercadoD.Application;
using MercadoD.Infrastructure;
using MercadoD.Persistence.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace MercadoD.Ioc
{
    public static class DependencyInjection
    {

        private const string HealthEndpointPath = "/health";
        private const string AlivenessEndpointPath = "/alive";

        /// <summary>
        /// Configuração padrão para aplicações web.
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static TBuilder AddWebServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            //builder.ConfigureOpenTelemetry();

            builder.AddDefaultHealthChecks();

            builder.Services.AddProblemDetails();
            builder.Services.AddServiceDiscovery();
            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                // Turn on resilience by default
                http.AddStandardResilienceHandler();

                // Turn on service discovery by default
                http.AddServiceDiscovery();
            });

            // Uncomment the following to restrict the allowed schemes for service discovery.
            // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
            // {
            //     options.AllowedSchemes = ["https"];
            // });

            return builder;
        }

        /// <summary>
        /// Adiciona injeção de dependência das demais camadas do DDD.
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static TBuilder AddDddInfrastructure<TBuilder>(this TBuilder builder)
            where TBuilder : IHostApplicationBuilder
        {
            //Injeção de dependência da cama de persistencia
            builder.AddPersistence();

            //Injeção de dependência da cama de aplicação
            builder.AddApplication();

            return builder;
        }

        public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddHealthChecks()
                // Add a default liveness check to ensure app is responsive
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

            return builder;
        }

        public static WebApplication MapDefaultEndpoints(this WebApplication app)
        {
            // Adding health checks endpoints to applications in non-development environments has security implications.
            // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
            if (app.Environment.IsDevelopment())
            {
                // All health checks must pass for app to be considered ready to accept traffic after starting
                app.MapHealthChecks(HealthEndpointPath);

                // Only health checks tagged with the "live" tag must pass for app to be considered alive
                app.MapHealthChecks(AlivenessEndpointPath, new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("live")
                });
            }

            return app;
        }
    }
}
