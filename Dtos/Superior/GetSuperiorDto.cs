namespace hr_system_backend.Dtos
{
  public class GetSuperiorDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Area { get; set; }
    public string Position { get; set; }
    public Role Role { get; set; }
    public int Daysoff { get; set; }
    public Status Status { get; set; }
    //Navigations
    public List<GetEmployeeForSuperiorDto> Employees { get; set; }
  }
}