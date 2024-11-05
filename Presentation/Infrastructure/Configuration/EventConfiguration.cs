using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(nameof(Event));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.OccurredOn).IsRequired();
    }
}
