using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseUpdateMulti : Response
    {
        public List<ResponseUpdateModel> Data { get; set; }

        [JsonConstructor]
        public ResponseUpdateMulti(List<ResponseUpdateModel> data)
        {
            Data = data;
        }

        public ResponseUpdateMulti(List<ResponseUpdateModel> data, string message)
            : base(message)
        {
            Data = data;
        }

        public ResponseUpdateMulti(string message)
            : base(message)
        {
        }
    }
}
