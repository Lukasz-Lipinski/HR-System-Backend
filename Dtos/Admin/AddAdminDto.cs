namespace hr_system_backend.Dtos
{
  public class AddAdminDto : AdminCredSchema
  {
    public Role Role { get; set; }
    public AddAdminDto(AdminCredSchema newAdminData)
    {
      this.Name = newAdminData.Name;
      this.Surname = newAdminData.Surname;
      this.Password = newAdminData.Password;
      this.Email = newAdminData.Email;

      if (this.Role is not Role.Admin)
      {
        this.SetRole();
      }
    }

    private void SetRole()
    {
      this.Role = Role.Admin;
    }
  }
}