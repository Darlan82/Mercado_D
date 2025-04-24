using MercadoD.Domain.Loja;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Persistence.Sql
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

            context.SaveChanges();
        }
    }
}
