using MercadoD.Infrastructure.Time;

namespace MercadoD.Domain.Loja
{
    public class Loja : EntityBase
    {
        public string Nome { get; private set; }

        public Loja(string nome)
            : base()
        {        
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
    }
}
