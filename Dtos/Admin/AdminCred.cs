namespace hr_system_backend.Dtos
{
  public class AdminCred : AdminCredSchema
  {
    public AdminCred(string Name, string Surname, string Email, string Password)
    {
      this.Name = Name;
      this.Surname = Surname;
      this.Email = Email;
      this.Password = Password;
    }
  }
}