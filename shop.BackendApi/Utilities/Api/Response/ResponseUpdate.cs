using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseUpdate : Response
    {
        public ResponseUpdateModel Data { get; set; }

        public ResponseUpdate()
        {
        }

        [JsonConstructor]
        public ResponseUpdate(Guid id)
        {
            Data = new ResponseUpdateModel
            {
                Id = id
            };
        }

        public ResponseUpdate(Guid id, string message)
            : base(message)
        {
            Data = new ResponseUpdateModel
            {
                Id = id
            };
        }

        public ResponseUpdate(StatusCodeEnum code, string message, Guid id)
            : base(code, message)
        {
            Data = new ResponseUpdateModel
            {
                Id = id
            };
        }

        public ResponseUpdate(ResponseUpdateModel data)
        {
            Data = data;
        }
    }
}
