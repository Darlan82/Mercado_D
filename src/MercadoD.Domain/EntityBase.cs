using MercadoD.Infrastructure.Time;

namespace MercadoD.Domain
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public DateTime DtCriacao { get; protected set; }
        public DateTime? DtAlteracao { get; protected set; }
        public bool Inativo { get; protected set; }

        #region DomainEvents
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();        
        #endregion

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            DtCriacao = Clock.UtcNow;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => this._domainEvents.Clear();

        public void AlterarDataAlteracao()
        {
            DtAlteracao = Clock.UtcNow;
        }
    }
}
