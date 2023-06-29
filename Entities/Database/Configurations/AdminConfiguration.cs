using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hr_system_backend.Entities.Database.Configurations
{
  public class AdminConfiguration : IEntityTypeConfiguration<Admin>
  {
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
      builder.Property(a => a.Email).IsRequired();
      builder.Property(a => a.Name).IsRequired();
      builder.Property(a => a.Surname).IsRequired();
      builder.Property(a => a.Password).IsRequired();
      builder.Property(a => a.Role).IsRequired();
      builder.Property(a => a.Id).IsRequired();
      builder.HasData(new Admin
      {
        Id = Guid.NewGuid(),
        Email = "test@test.com",
        Name = "test",
        Surname = "test",
        Password = BCrypt.Net.BCrypt.HashPassword("test"),
        Role = Role.Admin
      });
    }
  }
}