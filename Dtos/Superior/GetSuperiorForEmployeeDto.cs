namespace hr_system_backend.Dtos
{
  public class GetSuperiorForEmployeeDto
  {
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Area { get; set; }
    public string Position { get; set; }
  }
}