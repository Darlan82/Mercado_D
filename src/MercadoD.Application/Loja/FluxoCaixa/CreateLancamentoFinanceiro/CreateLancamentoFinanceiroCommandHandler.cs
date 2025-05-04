using MassTransit;
using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Common.Data;
using Microsoft.Extensions.Logging;
using MassTransit.Transports;
using MercadoD.Domain.Loja.FluxoCaixa.DomainEvents;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    internal sealed class CreateLancamentoFinanceiroCommandHandler : IConsumer<CreateLancamentoFinanceiroCommand>
    {
        private readonly ILancamentoFinanceiroRepository _lancamentoFinanceiroRepository;
        private readonly IContaFinanceiraRepository _contaFinanceiraRepository;
        private readonly ILogger<CreateLancamentoFinanceiroCommandHandler> _logger;
        private readonly IUnitWork _unitWork;

        public CreateLancamentoFinanceiroCommandHandler(ILancamentoFinanceiroRepository lancamentoFinanceiroRepository, 
            IContaFinanceiraRepository contaFinanceiraRepository, 
            ILogger<CreateLancamentoFinanceiroCommandHandler> logger, IUnitWork unitWork)
        {
            _lancamentoFinanceiroRepository = lancamentoFinanceiroRepository ?? throw new ArgumentNullException(nameof(lancamentoFinanceiroRepository));
            _contaFinanceiraRepository = contaFinanceiraRepository ?? throw new ArgumentNullException(nameof(contaFinanceiraRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitWork = unitWork ?? throw new ArgumentNullException(nameof(unitWork));
        }

        public async Task Consume(ConsumeContext<CreateLancamentoFinanceiroCommand> command)
        {
            var contaFinanceira = await _contaFinanceiraRepository.GetByIdAsync(command.Message.ContaId);
            if (contaFinanceira == null)
            {
                await command.RespondAsync(new CreateLancamentoFinanceiroCommandResponseError("Conta financeira não encontrada."));
                return;
            }

            var lancamento = LancamentoFinanceiro.Create(
                command.Message.ContaId,
                command.Message.Valor,
                command.Message.Descricao,
                command.Message.DtLancamento,
                command.Message.DtVencimento,
                command.Message.DtPagamento);

            await _lancamentoFinanceiroRepository.InsertAsync(lancamento);

            await _unitWork.SaveAsync();

            await command.RespondAsync(new CreateLancamentoFinanceiroCommandResponse(lancamento.Id));            

            _logger.LogInformation("Lançamento financeiro criado: {LancamentoId}, {ContaId}", lancamento.Id, lancamento.ContaId);
        }
    }
}
