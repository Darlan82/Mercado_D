namespace MercadoD.Domain.Entities
{
    public abstract class EntityPadrao
    {
        public Guid Id { get; protected set; }
        public DateTime DtCriacao { get; protected set; }
        public DateTime? DtAlteracao { get; protected set; }
        public bool Inativo { get; protected set; }

        protected EntityPadrao()
        {
            Id = Guid.NewGuid();
            DtCriacao = DateTime.UtcNow;
        }

        public void AlterarDataAlteracao()
        {
            DtAlteracao = DateTime.UtcNow;
        }
    }
}
