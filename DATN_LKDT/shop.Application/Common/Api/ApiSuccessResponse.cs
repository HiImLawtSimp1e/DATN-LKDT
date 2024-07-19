namespace shop.Application.Common;

public class ApiSuccessResponse<T> : ApiResponse<T>
{
    public ApiSuccessResponse(string message)
    {
        Success = true;
        Message = message;
    }

    public ApiSuccessResponse(T data)
    {
        Success = true;
        Data = data;
    }

    public ApiSuccessResponse(string message, T data)
    {
        Success = true;
        Message = message;
        Data = data;
    }
}