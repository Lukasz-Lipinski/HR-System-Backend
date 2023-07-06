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
    public async Task<List<Superior>> FindByNameOrSurname(string phrase)
    {
      var superiors = await this.dbContext.Superiors
        .AsNoTracking()
        .Where(s => s.Name.ToLower().Contains(phrase.ToLower()) || s.Surname.ToLower().Contains(phrase.ToLower()))
        .Include(s => s.Employees)
        .ToListAsync();

      return superiors.Count == 0 ? null : superiors;
    }

    public async Task<List<Superior>> FindByPosition(string position)
    {
      var superiors = await this.dbContext.Superiors
          .AsNoTracking()
          .Where(s => s.Position.ToLower().Contains(position.ToLower()))
          .Include(s => s.Employees)
          .ToListAsync();
      return superiors.Count == 0 ? null : superiors; ;


    }

    public async Task<List<Superior>> GetAllSuperiors()
    {
      return await this.dbContext.Superiors.AsNoTracking().Include(s => s.Employees).ToListAsync();
    }

  }
}