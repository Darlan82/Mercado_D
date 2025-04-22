namespace MercadoD.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using MercadoD.Domain.Entities;

/// <summary>
/// Contexto de dados para as entidades do MercadoD.
/// </summary>
public class MercadoDbContext : DbContext
{
    public MercadoDbContext(DbContextOptions<MercadoDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Conjunto de lançamentos financeiros.
    /// </summary>
    public DbSet<LancamentoFinanceiro> Lancamentos { get; set; } = null!;
}