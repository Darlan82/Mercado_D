namespace MercadoD.API.Models;

using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO para criação de novo lançamento financeiro.
/// </summary>
public class CreateLancamentoDto
{
    /// <summary>Valor monetário do lançamento.</summary>
    [Required]
    //[Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero.")]
    public decimal Valor { get; set; }
        
    /// <summary>Data e hora do lançamento.</summary>
    [Required]
    public DateTime DataHora { get; set; }

    /// <summary>Identificador da loja.</summary>
    //[Required(AllowEmptyStrings = false, ErrorMessage = "LojaId é obrigatório.")]
    [Required]
    public Guid LojaId { get; set; }

    /// <summary>Descrição opcional.</summary>
    public string? Descricao { get; set; }
}