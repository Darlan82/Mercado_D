using MassTransit;
using MercadoD.Domain.Loja.FluxoCaixa;

namespace MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira
{
    internal class GetPagedContaFinanceiraHandler : IConsumer<GetAllContaFinanceiraQuery>
    {
        private readonly IContaFinanceiraRepository _repository;

        public GetPagedContaFinanceiraHandler(IContaFinanceiraRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<GetAllContaFinanceiraQuery> context)
        {
            var result = await _repository.GetAllAsync(context.Message.PaginaAtual, context.Message.QtdRegistros);

            var lsContasDto = new List<ContaFinanceiraDto>(result.Registros.Count());
            foreach (var conta in result.Registros)
            {
                var dto = new ContaFinanceiraDto(conta.Id, conta.Nome, conta.SaldoPrevisto, conta.SaldoRealizado,
                    (int)conta.Tipo, conta.Tipo.ToString(), conta.LojaId, conta.Loja.Nome);
                lsContasDto.Add(dto);
            }

            await context.RespondAsync(new MercadoD.Application.Data.PagedResult<ContaFinanceiraDto>(context.Message.PaginaAtual, context.Message.QtdRegistros,
                result.QtdTotal, lsContasDto));
        }
    }
}
