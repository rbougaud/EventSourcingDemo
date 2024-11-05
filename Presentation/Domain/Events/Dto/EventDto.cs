using MediatR;

namespace Domain.Events.Dto;

public abstract record EventDto : INotification
{
    public abstract Guid Id { get; }
    public abstract string Type { get; }
    public string? Data { get; init; }
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
