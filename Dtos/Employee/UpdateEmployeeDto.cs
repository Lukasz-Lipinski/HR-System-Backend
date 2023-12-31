namespace hr_system_backend.Dtos
{
  public class UpdateEmployeeDto
  {
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public string Position { get; set; }
    public int Daysoff { get; set; }
    public Status Status { get; set; }
    public GetSuperiorForEmployeeDto superior { get; set; }
  }
}