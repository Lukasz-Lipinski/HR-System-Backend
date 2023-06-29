namespace hr_system_backend.Entities
{
  public class User : Person
  {
    public string Position { get; set; }
    public int Daysoff { get; set; }
    public string Superior { get; set; }
    public Status Status { get; set; }
  }
}