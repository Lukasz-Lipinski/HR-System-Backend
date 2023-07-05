namespace hr_system_backend.Entities
{
  public class Admin : Person
  {
    public string Password { get; set; } = string.Empty;
  }
}