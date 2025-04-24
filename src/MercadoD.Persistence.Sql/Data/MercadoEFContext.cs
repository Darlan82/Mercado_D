using MercadoD.Domain.Loja;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace MercadoD.Persistence.Sql.Data
{
    internal class MercadoEFContext : DbContext
    {
        //private readonly IHostEnvironment _env;

        public MercadoEFContext(DbContextOptions<MercadoEFContext> options)
        : base(options)
        {
            //_env = env;            
        }

        public DbSet<Loja> Lojas { get; set; } = null!;
        public DbSet<ContaFinanceira> ContasFinanceiras { get; set; } = null!;
        public DbSet<LancamentoFinanceiro> LancamentosFinanceiros { get; set; } = null!;
        public DbSet<SaldoConsolidadoDiario> SaldosConsolidadosDiario { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    if (!optionsBuilder.IsConfigured && _env.IsDevelopment())
        //    {
        //        optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
        //        {
        //            await DbDevInitializer.InitializeAsync(context);
        //        });
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapping for Loja  
            modelBuilder.Entity<Loja>(entity =>
            {
                entity.ToTable("Lojas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            });

            // Mapping for ContaFinanceira  
            modelBuilder.Entity<ContaFinanceira>(entity =>
            {
                entity.ToTable("ContasFinanceiras");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SaldoPrevisto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Tipo).IsRequired();
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
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DtVencimento).IsRequired();
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
                entity.Property(e => e.Data).IsRequired();
                entity.Property(e => e.SaldoPrevisto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SaldoRealizado).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Conta)
                      .WithMany()
                      .HasForeignKey(e => e.ContaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

    
}
