using MassTransit;
using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    internal sealed class CreateLancamentoFinanceiroCommandHandler : IConsumer<CreateLancamentoFinanceiroCommand>
    {
        private readonly ILancamentoFinanceiroRepository _lancamentoFinanceiroRepository;
        private readonly ILogger<CreateLancamentoFinanceiroCommandHandler> _logger;
        private readonly IUnitWork _unitWork;

        public CreateLancamentoFinanceiroCommandHandler(ILancamentoFinanceiroRepository lancamentoFinanceiroRepository, 
            ILogger<CreateLancamentoFinanceiroCommandHandler> logger, IUnitWork unitWork)
        {
            _lancamentoFinanceiroRepository = lancamentoFinanceiroRepository ?? throw new ArgumentNullException(nameof(lancamentoFinanceiroRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitWork = unitWork ?? throw new ArgumentNullException(nameof(unitWork));
        }

        public async Task Consume(ConsumeContext<CreateLancamentoFinanceiroCommand> command)
        {
            var lancamento = LancamentoFinanceiro.Create(
                command.Message.ContaId,
                command.Message.Valor,
                command.Message.Descricao,
                command.Message.DtVencimento);

            await _lancamentoFinanceiroRepository.InsertAsync(lancamento);

            await _unitWork.SaveAsync();

            await command.RespondAsync(new CreateLancamentoFinanceiroCommandResponse(lancamento.Id));

            _logger.LogInformation("Lançamento financeiro criado: {LancamentoId}, {ContaId}", lancamento.Id, lancamento.ContaId);
        }
    }
}
