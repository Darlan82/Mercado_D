using MercadoD.Infrastructure.Repositories;
using MercadoD.Infrastructure.ValueType;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public interface ISaldoConsolidadoDiarioRepository : IRepositoryBase<SaldoConsolidadoDiario>
    {
        Task<SaldoConsolidadoDiario?> GetByContaIdDate(Guid contaId, DayStamp dayStamp);
    }
}
