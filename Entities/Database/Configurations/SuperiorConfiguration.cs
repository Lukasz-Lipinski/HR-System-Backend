using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hr_system_backend.Entities.Database.Configurations
{
  public class SuperiorConfiguration : IEntityTypeConfiguration<Superior>
  {
    public void Configure(EntityTypeBuilder<Superior> builder)
    {
      builder.HasMany(s => s.Employees).WithOne(e => e.Superior).HasForeignKey(s => s.SuperiorId);
      builder.HasData(new Superior
      {
        Area = "Casting",
        Daysoff = 26,
        Email = "superior@example.com",
        Id = Guid.NewGuid(),
        Name = "Superior name",
        Surname = "Superior surname",
        Position = "Supirior Manager",
        Role = Role.Manager,
        Status = Status.Employed,
      });
    }
  }
}