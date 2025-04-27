using MercadoD.Domain.Loja;
using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Infrastructure.ValueType;
using Microsoft.EntityFrameworkCore;


namespace MercadoD.Persistence.Sql.Data
{
    internal class MercadoEFContext : DbContext
    {
        public MercadoEFContext(DbContextOptions<MercadoEFContext> options)
        : base(options)
        {        
        }

        public DbSet<Loja> Lojas { get; set; } = null!;
        public DbSet<ContaFinanceira> ContasFinanceiras { get; set; } = null!;
        public DbSet<LancamentoFinanceiro> LancamentosFinanceiros { get; set; } = null!;
        public DbSet<SaldoConsolidadoDiario> SaldosConsolidadosDiario { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapping for Loja  
            modelBuilder.Entity<Loja>(entity =>
            {
                entity.ToTable("Lojas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired()
                    .HasMaxLength(100);
            });

            // Mapping for ContaFinanceira  
            modelBuilder.Entity<ContaFinanceira>(entity =>
            {
                entity.ToTable("ContasFinanceiras");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired()
                    .HasMaxLength(FluxoCaixaConstants.ContaFinanceira.NomeMaxLength);
                entity.Property(e => e.SaldoPrevisto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SaldoRealizado).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Tipo).IsRequired();
                entity.Property(e => e.Version).IsRowVersion();
                entity.HasOne(e => e.Loja)
                      .WithMany()
                      .HasForeignKey(e => e.LojaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapping for LancamentoFinanceiro  
            modelBuilder.Entity<LancamentoFinanceiro>(entity =>
            {
                entity.ToTable("LancamentosFinanceiros");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Valor).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Descricao).IsRequired()
                    .HasMaxLength(FluxoCaixaConstants.LancamentoFinanceiro.DescricaoMaxLength);
                entity.Property(e => e.DtVencimento).IsRequired();
                entity.Property(e => e.DtPagamento);
                entity.Property(e => e.DtLancamento).IsRequired();
                entity.Property(e => e.SaldoPrevistoContabilizado).IsRequired();
                entity.Property(e => e.SaldoRealizadoContabilizado).IsRequired();
                entity.Property(e => e.Version).IsRowVersion();
                entity.HasOne(e => e.Conta)
                      .WithMany()
                      .HasForeignKey(e => e.ContaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapping for SaldoConsolidadoDiario  
            modelBuilder.Entity<SaldoConsolidadoDiario>(entity =>
            {
                entity.ToTable("SaldosConsolidadosDiario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Data)                    
                    .HasConversion(
                        d => d.Value,
                        v => DayStamp.FromInt(v))
                    .HasColumnType("int")
                    .IsRequired();
                entity.Property(e => e.SaldoPrevisto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SaldoRealizado).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Version).IsRowVersion();
                entity.HasOne(e => e.Conta)
                      .WithMany()
                      .HasForeignKey(e => e.ContaId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.ContaId);
                entity.HasIndex(e => new { e.ContaId, e.Data })
                    .IsUnique()
                    .HasDatabaseName("IX_Conta_Data")
                    ;
            });
        }
    }

    
}
