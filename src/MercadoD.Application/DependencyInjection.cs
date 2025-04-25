using FluentValidation;
using MassTransit;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro;
using MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira;
using MercadoD.Application.Loja.FluxoCaixa.GetLancamentoFinanceiro;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MercadoD.Application
{
    public static class MassTransitInjection
    {
        public static TBuilder AddApplication<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            // registra todos os validators do assembly Application
            builder.Services.AddValidatorsFromAssemblyContaining<IApplicationValidator>(ServiceLifetime.Transient);            

            builder.Services.AddMediator(cfg =>
            {
                cfg.AddConsumer<CreateLancamentoFinanceiroCommandHandler>();
                cfg.AddConsumer<GetLancamentoFinanceiroHandler>();
                cfg.AddConsumer<GetContaFinanceiraHandler>();
                cfg.AddConsumer<GetPagedContaFinanceiraHandler>();

                
                cfg.AddRequestClient<GetAllContaFinanceiraQuery>();
                cfg.AddRequestClient<GetContaFinanceiraQuery>();

                // middleware reutilizável (opcional)
                //cfg.UseMessageRetry(r => r.Interval(3, 500));
            });

            return builder;
        }
    }
}
