using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseError : Response
    {
        public List<Dictionary<string, string>> ErrorDetail { get; set; }

        [JsonConstructor]
        public ResponseError(StatusCodeEnum code, string message, List<Dictionary<string, string>> errorDetail = null)
            : base(code, message)
        {
            ErrorDetail = errorDetail;
        }
    }
}
