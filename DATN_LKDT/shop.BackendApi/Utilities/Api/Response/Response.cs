using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model;


public class Response
{
    public StatusCodeEnum Code { get; set; } = StatusCodeEnum.Success;


    public string Message { get; set; } = "Success";


    public string LicenseInfo { get; set; } = "invalid";


    public long TotalTime { get; set; }

    [JsonConstructor]
    public Response(StatusCodeEnum code, string message)
    {
        Code = code;
        Message = message;
    }

    public Response(string message)
    {
        Message = message;
    }

    public Response()
    {
    }
}
