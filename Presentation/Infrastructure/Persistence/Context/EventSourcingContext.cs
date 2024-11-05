using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class EventSourcingContext : DbContext
{
    public EventSourcingContext(DbContextOptions<EventSourcingContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Ne rien faire ici si vous configurez déjà DbContext dans Startup.cs
    }
}
