namespace LMS.BLL.DTOs.Response;

public class Status
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object Data { get; set; }
}
