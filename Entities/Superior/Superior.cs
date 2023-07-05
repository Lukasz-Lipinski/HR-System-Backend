namespace hr_system_backend.Entities
{
  public class Superior : Person
  {
    public string Position { get; set; }
    public int Daysoff { get; set; }
    public Status Status { get; set; }
    public string Area { get; set; }
    //Navigations
    public List<Employee> Employees { get; set; }
  }
}