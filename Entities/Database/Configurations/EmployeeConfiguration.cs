using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hr_system_backend.Entities.Database.Configurations
{
  public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
  {
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
      Guid superiorID = Guid.NewGuid();

      builder.Property(e => e.Daysoff).IsRequired();
      builder.Property(e => e.Email).IsRequired();
      builder.Property(e => e.Name).IsRequired();
      builder.Property(e => e.Role).IsRequired();
      builder.Property(e => e.Status).IsRequired();
      builder.Property(e => e.Id).IsRequired();
      builder.Property(e => e.Surname).IsRequired();

    }
  }
}