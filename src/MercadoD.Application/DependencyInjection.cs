using FluentValidation;
using MassTransit;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro.DomainEventHandlers;
using MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira;
using MercadoD.Application.Loja.FluxoCaixa.GetLancamentoFinanceiro;
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
            //cfg.AddConsumers(typeof(CreateLancamentoFinanceiroCommandHandler).Assembly);

            cfg.AddConsumer<CreateLancamentoFinanceiroCommandHandler>();
            cfg.AddConsumer<GetLancamentoFinanceiroHandler>();
            cfg.AddConsumer<GetContaFinanceiraHandler>();
            cfg.AddConsumer<GetPagedContaFinanceiraHandler>();


            cfg.AddRequestClient<GetAllContaFinanceiraQuery>();
            cfg.AddRequestClient<GetContaFinanceiraQuery>();

            //Eventos de Domínio
            cfg.AddConsumer<LancamentoFinanceiroCreatedDomainEventHandler>();

            // middleware reutilizável (opcional)
            //cfg.UseMessageRetry(r => r.Interval(3, 500));        
        }

        public static void AddMediatorConsumersBus(IBusRegistrationConfigurator cfg)
        {
            //Eventos de Domínio
            cfg.AddConsumer<LancamentoFinanceiroCreatedDomainEventHandler>();            
        }

        public static void ConfigMessageBus(IBusFactoryConfigurator cfg, IBusRegistrationContext ctx)
        {
            // Endpoint para comandos de criação de lançamento financeiro
            //cfg.ReceiveEndpoint("lancamento-financeiro", e =>
            //{
            //    // Para comandos utilizamos um endpoint dedicado sem a topologia de publish/subscribe
            //    e.ConfigureConsumeTopology = false;
            //    e.ConfigureConsumer<CreateLancamentoFinanceiroCommandHandler>(ctx);
            //});

            // Endpoint para processar eventos de domínio
            cfg.ReceiveEndpoint("lancamento-financeiro", e =>
            {
                // Mantém a topologia padrão para que o subscription seja criado automaticamente
                e.ConfigureConsumer<LancamentoFinanceiroCreatedDomainEventHandler>(ctx);
            });

            
        }
    }
}
