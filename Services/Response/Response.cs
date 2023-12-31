namespace hr_system_backend.Services.Response
{
  public class Response<T>
  {
    public int Code { get; set; }
    public T Data { get; set; }
    public bool Error { get; set; }
    public string Message { get; set; }
  }
}