namespace hr_system_backend.Entities
{
  public class Employee : Person
  {
    public string Position { get; set; }
    public int Daysoff { get; set; }
    public Status Status { get; set; }
    public Superior Superior { get; set; }
    public Guid SuperiorId { get; set; }
  }
}