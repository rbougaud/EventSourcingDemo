using Domain.Entities;
using Infrastructure.Abstractions.Stores;
using Infrastructure.Persistence.Context;
using MediatR;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Stores;

public class EventStore(EventSourcingContext context, IMediator mediator) : IEventStore
{
    private readonly EventSourcingContext _context = context;
    private readonly IMediator _mediator = mediator;

    public async Task<bool> AppendAsync(Event @event)
    {
        var eventEntity = new Event
        {
            OccurredOn = @event.OccurredOn,
            Type = @event.Type,
            Data = JsonConvert.SerializeObject(@event)
        };

        _context.Events.Add(eventEntity);
        bool result = await _context.SaveChangesAsync() > 0;
        await _mediator.Publish(eventEntity); //Doit être l'objet correspondant au notification
        return result;
    }
}
