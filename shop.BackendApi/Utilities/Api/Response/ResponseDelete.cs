using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{

    public class ResponseDelete : Response
    {
        public ResponseDeleteModel Data { get; set; }

        public ResponseDelete()
        {
        }

        public ResponseDelete(Guid id)
        {
            Data = new ResponseDeleteModel
            {
                Id = id
            };
        }

        public ResponseDelete(string name)
        {
            Data = new ResponseDeleteModel
            {
                Name = name
            };
        }

        [JsonConstructor]
        public ResponseDelete(Guid id, string name)
        {
            Data = new ResponseDeleteModel
            {
                Id = id,
                Name = name
            };
        }

        public ResponseDelete(StatusCodeEnum code, string message, Guid id, string name)
            : base(code, message)
        {
            Data = new ResponseDeleteModel
            {
                Id = id,
                Name = name
            };
        }

        public ResponseDelete(ResponseDeleteModel data)
        {
            Data = data;
        }
    }
}
