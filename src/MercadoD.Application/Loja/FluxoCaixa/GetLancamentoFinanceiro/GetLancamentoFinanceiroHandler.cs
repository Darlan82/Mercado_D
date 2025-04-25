using MassTransit;
using MercadoD.Application.Data;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.Extensions.Logging;

namespace MercadoD.Application.Loja.FluxoCaixa.GetLancamentoFinanceiro
{
    internal class GetLancamentoFinanceiroHandler : IConsumer<GetLancamentoFinanceiroQuery>
    {
        private readonly ILancamentoFinanceiroRepository _lancamentoFinanceiroRepository;
        private readonly ILogger<CreateLancamentoFinanceiroCommandHandler> _logger;

        public GetLancamentoFinanceiroHandler(ILancamentoFinanceiroRepository lancamentoFinanceiroRepository
            ,ILogger<CreateLancamentoFinanceiroCommandHandler> logger
            )
        {
            _lancamentoFinanceiroRepository = lancamentoFinanceiroRepository ?? throw new ArgumentNullException(nameof(lancamentoFinanceiroRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<GetLancamentoFinanceiroQuery> query)
        {
            var lancamento = await _lancamentoFinanceiroRepository.GetByIdAsync(query.Message.id);

            if (lancamento == null)
            {
                await query.RespondAsync(new NotFoundResponse());
                return;
            }

            var dto = new LancamentoFinanceiroDto(lancamento.Id,
                lancamento.ContaId, lancamento.Conta.Nome, lancamento.Valor, lancamento.Descricao,
                lancamento.DtLancamento, lancamento.DtVencimento);

            await query.RespondAsync(dto);
        }
    }
}
