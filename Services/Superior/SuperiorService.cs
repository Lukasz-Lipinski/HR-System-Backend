using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public class SuperiorService : ISuperiorService
  {
    private readonly HRSystemDbContext dbContext;
    private readonly IMapper mapper;

    public SuperiorService(HRSystemDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }
    public async Task<List<Superior>> FindAllSuperiorsByName(string name)
    {
      var superiors = await this.dbContext.Superiors
        .AsNoTracking()
        .Where(s => s.Name.ToLower() == name.ToLower() || s.Name.ToLower().Contains(name.ToLower()))
        .Include(s => s.Employees)
        .ToListAsync();

      return superiors;
    }
  }
}