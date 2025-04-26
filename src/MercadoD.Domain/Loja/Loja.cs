namespace MercadoD.Domain.Loja
{
    public class Loja : EntityBase
    {
        public string Nome { get; private set; }

        #pragma warning disable CS8618 // Construtor para o EF
        private Loja() 
        { 
        }
        #pragma warning restore CS8618 

        public Loja(string nome)
            : base()
        {        
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
    }
}
