namespace shop.Application.Common;

public class ApiResponse<T>
{
    public bool IsSuccessed { get; set; }
    public string? Message { get; set; }
    public T? ResultObject { get; set; }
}
