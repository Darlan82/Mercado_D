namespace MercadoD.Domain.Entities;

/// <summary>
/// Representa o saldo consolidado por data e loja.
/// </summary>
public class SaldoConsolidado
{
    public DateTime Data { get; private set; }
    public string LojaId { get; private set; }
    public decimal TotalSaldo { get; private set; }

    /// <summary>
    /// Construtor para criação do saldo consolidado.
    /// </summary>
    /// <param name="data">Data do saldo consolidado.</param>
    /// <param name="lojaId">Identificador da loja relacionada.</param>
    /// <param name="totalSaldo">Valor total do saldo consolidado.</param>
    public SaldoConsolidado(DateTime data, string lojaId, decimal totalSaldo)
    {
        if (data == default) throw new ArgumentException("Data deve ser fornecida.", nameof(data));
        if (string.IsNullOrWhiteSpace(lojaId)) throw new ArgumentException("LojaId deve ser fornecida.", nameof(lojaId));

        Data = data;
        LojaId = lojaId;
        TotalSaldo = totalSaldo;
    }

    // Construtor vazio para compatibilidade com frameworks ORM.
    private SaldoConsolidado()
    {
        // Inicialização para evitar warnings de propriedade não nula
        LojaId = string.Empty;
    }
}
