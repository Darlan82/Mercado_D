using FluentValidation;
using MassTransit;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro.DomainEventHandlers;
using MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira;
using MercadoD.Application.Loja.FluxoCaixa.GetLancamentoFinanceiro;
using MercadoD.Domain;
using MercadoD.Domain.Loja.FluxoCaixa.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MercadoD.Application
{
    public static class DependencyInjection
    {
        public static TBuilder AddApplication<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            // registra todos os validators do assembly Application
            builder.Services.AddValidatorsFromAssemblyContaining<IApplicationValidator>(ServiceLifetime.Transient);

            return builder;
        }

        public static void AddMediatorConsumersLocal(IMediatorRegistrationConfigurator cfg)
        {
            //Exemplo caso seja automáticos para todos os tipos
            //cfg.AddConsumers(typeof(CreateLancamentoFinanceiroCommandHandler).Assembly);

            cfg.AddConsumer<CreateLancamentoFinanceiroCommandHandler>();
            cfg.AddConsumer<GetLancamentoFinanceiroHandler>();
            cfg.AddConsumer<GetContaFinanceiraHandler>();
            cfg.AddConsumer<GetPagedContaFinanceiraHandler>();

            cfg.AddRequestClient<GetAllContaFinanceiraQuery>();
            cfg.AddRequestClient<GetContaFinanceiraQuery>();

            // middleware reutilizável (opcional)
            //cfg.UseMessageRetry(r => r.Interval(3, 500));        
        }

        public static void AddMediatorConsumersBus(IBusRegistrationConfigurator cfg)
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            //Eventos de Domínio
            cfg.AddConsumer<LancamentoFinanceiroCreatedDomainEventHandler>();
        }

        public static void ConfigMessageBus(IBusFactoryConfigurator cfg, IBusRegistrationContext ctx)
        {
            cfg.Message<IDomainEvent>(m => m.SetEntityName("domain-event-entity"));

            #region Lançamento Financeiro            
            const string prefixLancFin = "lancamento-financeiro";
            cfg.Message<LancamentoFinanceiroCreatedDomainEvent>(m => m.SetEntityName(prefixLancFin + "-entity"));
            cfg.ReceiveEndpoint(prefixLancFin+ "-queue", e =>
            {
                e.ConfigureConsumer<LancamentoFinanceiroCreatedDomainEventHandler>(ctx);                
            });
            #endregion
        }
    }
}
