using MassTransit;
using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Domain.Loja.FluxoCaixa.DomainEvents;
using MercadoD.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro.DomainEventHandlers
{
    internal class LancamentoFinanceiroCreatedDomainEventHandler : IConsumer<LancamentoFinanceiroCreatedDomainEvent>
    {
        private readonly ILancamentoFinanceiroRepository _repository;
        private readonly ILogger<LancamentoFinanceiroCreatedDomainEventHandler> _logger;
        private readonly IUnitWork _unitWork;

        public LancamentoFinanceiroCreatedDomainEventHandler(ILancamentoFinanceiroRepository repository, 
            ILogger<LancamentoFinanceiroCreatedDomainEventHandler> logger, IUnitWork unitWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitWork = unitWork ?? throw new ArgumentNullException(nameof(unitWork));
        }

        public async Task Consume(ConsumeContext<LancamentoFinanceiroCreatedDomainEvent> context)
        {
            var lancamento = await _repository.GetByIdAsync(context.Message.id);
            if (lancamento == null)
                return;

            lancamento.Conta.ContabilizarSaldo(lancamento);

            await _unitWork.SaveAsync();
        }        
    }
}
