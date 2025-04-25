using MercadoD.Infrastructure.Time;

namespace MercadoD.Domain
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public DateTime DtCriacao { get; protected set; }
        public DateTime? DtAlteracao { get; protected set; }
        public bool Inativo { get; protected set; }
               

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            DtCriacao = Clock.UtcNow;
        }

        public void AlterarDataAlteracao()
        {
            DtAlteracao = Clock.UtcNow;
        }
    }
}
