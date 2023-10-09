namespace shop.Application.Common;

public class ApiSuccessResponse<T> : ApiResponse<T>
{
    public ApiSuccessResponse(string message)
    {
        IsSuccessed = true;
        Message = message;
    }

    public ApiSuccessResponse(T resultObject)
    {
        IsSuccessed = true;
        ResultObject = resultObject;
    }

    public ApiSuccessResponse(string message, T resultObject)
    {
        IsSuccessed = true;
        Message = message;
        ResultObject = resultObject;
    }
}