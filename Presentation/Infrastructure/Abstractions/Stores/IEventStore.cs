using Domain.Entities;

namespace Infrastructure.Abstractions.Stores;

public interface IEventStore
{
    Task<bool> AppendAsync(Event eventEntity);
}
