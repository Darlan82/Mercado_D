using MercadoD.Domain.Loja;
using MercadoD.Infrastructure.Data;
using MercadoD.Persistence.Sql.Data;
using MercadoD.Persistence.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.EntityFrameworkCore;

namespace MercadoD.Persistence.Sql
{
    public static class DependencyInjection
    {
        public static TBuilder AddPersistence<TBuilder>(this TBuilder builder) 
            where TBuilder : IHostApplicationBuilder
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            // Configura DbContext pela Aspire SQL Server
            //builder.AddSqlServerDbContext<MercadoEFContext>("mercadoD");
            builder.AddSqlServerDbContext<MercadoEFContext>("mercadoD",
                configureDbContextOptions: opts =>
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        opts.EnableDetailedErrors();

                        // se quiser executar um seed/ensure-created em dev:
                        opts.LogTo(Console.WriteLine);
                    }
                });

            // Configura DbContext com SQL Server
            //services.AddDbContext<MercadoEFContext>(options =>
            //    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));

            // Registra infra
            services.AddScoped<IUnitWork, UnitWork>();
            services.AddScoped<IDataContext, MercadoDataContext>();

            // Registra repositórios
            services.AddScoped<ILojaRepository, LojaRepository>();

            return builder;
        }

        public static async Task ApplyMigration<THost>(this THost host)
            where THost : IHost
        {
            await using var scope = host.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<MercadoEFContext>();

            await db.Database.MigrateAsync();

            var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

            if (env.IsDevelopment())
            {
                await DbDevInitializer.InitializeAsync(db);
            }
        }
    }
}
