using MassTransit;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.Extensions.Logging;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    internal sealed class CreateLancamentoFinanceiroCommandHandler : IConsumer<CreateLancamentoFinanceiroCommand>
    {
        private readonly ILancamentoFinanceiroRepository _lancamentoFinanceiroRepository;
        private readonly ILogger<CreateLancamentoFinanceiroCommandHandler> _logger;

        public CreateLancamentoFinanceiroCommandHandler(ILancamentoFinanceiroRepository lancamentoFinanceiroRepository,
            ILogger<CreateLancamentoFinanceiroCommandHandler> logger)
        {
            _lancamentoFinanceiroRepository = lancamentoFinanceiroRepository ?? throw new ArgumentNullException(nameof(lancamentoFinanceiroRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<CreateLancamentoFinanceiroCommand> command)
        {
            var lancamento = new LancamentoFinanceiro(
                command.Message.ContaId,
                command.Message.Valor,
                command.Message.Descricao,
                command.Message.DtVencimento);

            await _lancamentoFinanceiroRepository.InsertAsync(lancamento);

            await command.RespondAsync(new CreateLancamentoFinanceiroCommandResponse(lancamento.Id));

            _logger.LogInformation("Lançamento financeiro criado: {LancamentoId}, {ContaId}", lancamento.Id, lancamento.ContaId);
        }
    }
}
