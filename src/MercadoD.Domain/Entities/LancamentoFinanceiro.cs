namespace MercadoD.Domain.Entities;

using MercadoD.Domain.Enums;

/// <summary>
/// Representa um lançamento financeiro de débito ou crédito.
/// </summary>
public class LancamentoFinanceiro
{
    public Guid Id { get; private set; }
    public decimal Valor { get; private set; }
    public TipoLancamento Tipo { get; private set; }
    public DateTime DataHora { get; private set; }
    public string? Descricao { get; private set; }
    public string LojaId { get; private set; }

    /// <summary>
    /// Construtor para criação de um lançamento financeiro.
    /// </summary>
    /// <param name="id">Identificador único do lançamento.</param>
    /// <param name="valor">Valor monetário do lançamento.</param>
    /// <param name="tipo">Tipo do lançamento (débito ou crédito).</param>
    /// <param name="dataHora">Data e hora da ocorrência do lançamento.</param>
    /// <param name="lojaId">Identificador da loja relacionada.</param>
    /// <param name="descricao">Descrição opcional do lançamento.</param>
    public LancamentoFinanceiro(Guid id, decimal valor, TipoLancamento tipo, DateTime dataHora, string lojaId, string? descricao = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id não pode ser vazio.", nameof(id));
        if (valor <= 0) throw new ArgumentException("Valor deve ser maior que zero.", nameof(valor));
        if (!Enum.IsDefined(typeof(TipoLancamento), tipo)) throw new ArgumentException("Tipo de lançamento inválido.", nameof(tipo));
        if (dataHora == default) throw new ArgumentException("DataHora deve ser fornecida.", nameof(dataHora));
        if (string.IsNullOrWhiteSpace(lojaId)) throw new ArgumentException("LojaId deve ser fornecida.", nameof(lojaId));
        if (descricao != null && string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException("Descricao não pode ser vazia se fornecida.", nameof(descricao));

        Id = id;
        Valor = valor;
        Tipo = tipo;
        DataHora = dataHora;
        LojaId = lojaId;
        Descricao = descricao;
    }

    // Construtor vazio para compatibilidade com frameworks ORM.
    private LancamentoFinanceiro()
    {
        // Inicialização para evitar warnings de propriedade não nula
        LojaId = string.Empty;
    }
}
