using MassTransit;
using MercadoD.Application.Data;
using MercadoD.Domain.Loja.FluxoCaixa;

namespace MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira
{
    internal class GetContaFinanceiraHandler : IConsumer<GetContaFinanceiraQuery>
    {
        private readonly IContaFinanceiraRepository _repository;

        public GetContaFinanceiraHandler(IContaFinanceiraRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<GetContaFinanceiraQuery> context)
        {
            var conta = await _repository.GetByIdAsync(context.Message.id);
            if (conta == null)
            {
                await context.RespondAsync(new NotFoundResponse());
                return;
            }

            var dto = new ContaFinanceiraDto(conta.Id, conta.Nome, conta.SaldoPrevisto, conta.SaldoRealizado,
                (int)conta.Tipo, conta.Tipo.ToString(), conta.LojaId, conta.Loja.Nome);

            await context.RespondAsync(dto);
        }
    }
}
