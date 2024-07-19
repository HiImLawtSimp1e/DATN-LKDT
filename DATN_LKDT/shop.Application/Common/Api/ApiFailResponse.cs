namespace shop.Application.Common;

public class ApiFailResponse<T> : ApiResponse<T>
{
    public string[] ValidationErrors { get; set; }

    public ApiFailResponse(string message)
    {
        Success = false;
        Message = message;
    }

    public ApiFailResponse(string message, string[] validationErrors)
    {
        Success = false;
        Message = message;
        ValidationErrors = validationErrors;
    }
}