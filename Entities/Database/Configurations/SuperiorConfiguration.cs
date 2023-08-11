using hr_system_backend.Services.FakeData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hr_system_backend.Entities.Database.Configurations
{
  public class SuperiorConfiguration : IEntityTypeConfiguration<Superior>
  {
    public void Configure(EntityTypeBuilder<Superior> builder)
    {
      builder.HasMany(s => s.Employees).WithOne(e => e.Superior).HasForeignKey(s => s.SuperiorId);
    }
  }
}