using MassTransit;
using MercadoD.Domain;
using MercadoD.Infra.Persistence.Sql.Data;

namespace MercadoD.Infra.Persistence.Sql.Filters
{
    internal class EventsFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly MercadoEFContext _context;
        private readonly IPublishEndpoint _sendEndpointProvider;

        public EventsFilter(MercadoEFContext context, IPublishEndpoint sendEndpointProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _sendEndpointProvider = sendEndpointProvider ?? throw new ArgumentNullException(nameof(sendEndpointProvider));
        }

        public void Probe(ProbeContext context) { }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);

            var entities = _context.ChangeTracker.Entries<EntityBase>()
                .Where(e => e.Entity.DomainEvents is not null && e.Entity.DomainEvents.Any());

            var events = entities.SelectMany(x => x.Entity.DomainEvents).ToList();
            entities.ToList().ForEach(x => x.Entity.ClearDomainEvents());

            foreach (var entity in events)
            {
                var type = entity.GetType();
                await _sendEndpointProvider.Publish(entity, type);
            }
        }
    }
}
