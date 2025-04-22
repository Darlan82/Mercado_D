namespace MercadoD.Application.Services;

using MercadoD.Domain.Entities;

/// <summary>
/// Serviço de aplicação para registro de lançamentos financeiros.
/// </summary>
public interface ILancamentoService
{
    /// <summary>
    /// Registra um novo lançamento financeiro.
    /// </summary>
    /// <param name="lancamento">Lançamento a ser registrado.</param>
    Task RegistrarLancamentoAsync(LancamentoFinanceiro lancamento);
}