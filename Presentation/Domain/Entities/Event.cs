using MediatR;

namespace Domain.Entities;

public class Event : INotification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Type { get; set; }
    public string? Data { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}
