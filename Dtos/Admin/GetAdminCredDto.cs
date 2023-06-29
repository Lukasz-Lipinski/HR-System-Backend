namespace hr_system_backend.Dtos
{
  public class GetAdminCredDto
  {
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public Role role;
  }
}