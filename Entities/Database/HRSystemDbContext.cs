using hr_system_backend.Services.FakeData;

namespace hr_system_backend.Entities.Database
{
  public class HRSystemDbContext : DbContext
  {
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Superior> Superiors { get; set; }
    public HRSystemDbContext(DbContextOptions<HRSystemDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

      /**
        Seeding data to DB
        If you'd like to seed data, uncomment the lines below

        FakeData.GenerateFakeSuperiors();
        FakeData.GenerateFakeEmployees();

        modelBuilder.Entity<Superior>().HasData(FakeData.Superiors);
        modelBuilder.Entity<Employee>().HasData(FakeData.Employees);
      */
    }
  }
}