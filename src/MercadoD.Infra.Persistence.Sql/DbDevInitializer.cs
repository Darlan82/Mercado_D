using MercadoD.Domain.Loja;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Infra.Persistence.Sql
{
    internal static class DbDevInitializer
    {
        public static async Task InitializeAsync(DbContext context)
        {
            //context.Database.EnsureCreated();

            var setLoja = context.Set<Loja>();

            var loja1Nome = "Loja 1";
            var loja1 = await setLoja.FirstOrDefaultAsync(l => l.Nome == loja1Nome);
            if (loja1 == null)
            {
                loja1 = new Loja("Loja 1");
                setLoja.Add(loja1);
            }

            var setConta = context.Set<ContaFinanceira>();

            var ccDespesasInternas = await setConta.FirstOrDefaultAsync(c => c.Nome == "Despesas internas");
            if (ccDespesasInternas == null)
            {
                ccDespesasInternas = new ContaFinanceira(loja1.Id, "Despesas internas", ContaFinanceiraTipo.APagar);
                setConta.Add(ccDespesasInternas);
            }

            var ccFornecedores = await setConta.FirstOrDefaultAsync(c => c.Nome == "Fornecedores");
            if (ccFornecedores == null)
            {
                ccFornecedores = new ContaFinanceira(loja1.Id, "Fornecedores", ContaFinanceiraTipo.AReceber);
                setConta.Add(ccFornecedores);
            }

            await context.SaveChangesAsync();
        }
    }
}
