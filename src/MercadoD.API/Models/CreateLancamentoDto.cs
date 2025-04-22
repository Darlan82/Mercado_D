namespace MercadoD.API.Models;

using System;
using System.ComponentModel.DataAnnotations;
using MercadoD.Domain.Enums;

/// <summary>
/// DTO para criação de novo lançamento financeiro.
/// </summary>
public class CreateLancamentoDto
{
    /// <summary>Valor monetário do lançamento.</summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    /// <summary>Tipo do lançamento (Debito ou Credito).</summary>
    [Required]
    public TipoLancamento Tipo { get; set; }

    /// <summary>Data e hora do lançamento.</summary>
    [Required]
    public DateTime DataHora { get; set; }

    /// <summary>Identificador da loja.</summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "LojaId é obrigatório.")]
    public string LojaId { get; set; } = null!;

    /// <summary>Descrição opcional.</summary>
    public string? Descricao { get; set; }
}