using MercadoD.Common.Repositories;
using MercadoD.Common.ValueType;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public interface ISaldoConsolidadoDiarioRepository : IRepositoryBase<SaldoConsolidadoDiario>
    {
        Task<SaldoConsolidadoDiario?> GetByContaIdDate(Guid contaId, DayStamp dayStamp);
    }
}
