using MercadoD.Domain.Entities;

namespace MercadoD.Domain.Loja
{
    public class Loja : EntityPadrao
    {
        public string Nome { get; private set; }

        public Loja(string nome)
            : base()
        {        
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
    }
}
