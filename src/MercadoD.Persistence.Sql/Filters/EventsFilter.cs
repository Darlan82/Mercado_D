using MassTransit;
using MassTransit.Mediator;
using MercadoD.Domain;
using MercadoD.Persistence.Sql.Data;

namespace MercadoD.Persistence.Sql.Filters
{
    internal class EventsFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly MercadoEFContext _context;
        private readonly IMediator _mediator;

        public EventsFilter(MercadoEFContext context, IMediator mediator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
                await _mediator.Publish(entity);
            }
        }
    }
}
