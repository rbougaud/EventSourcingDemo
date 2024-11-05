using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(nameof(Student));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
    }
}
