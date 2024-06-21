using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseObject<T> : Response
    {
        public T Data { get; set; }

        public ResponseObject()
        {
        }

        [JsonConstructor]
        public ResponseObject(T data)
        {
            Data = data;
        }

        public ResponseObject(T data, string message)
        {
            Data = data;
            base.Message = message;
        }

        public ResponseObject(T data, string message, StatusCodeEnum code)
        {
            base.Code = code;
            Data = data;
            base.Message = message;
        }
    }
}
