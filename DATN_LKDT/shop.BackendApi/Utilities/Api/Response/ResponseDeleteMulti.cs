using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseDeleteMulti : Response
    {
        public IEnumerable<ResponseDeleteModel> Data { get; set; }

        [JsonConstructor]
        public ResponseDeleteMulti(IEnumerable<ResponseDeleteModel> data)
        {
            Data = data;
        }

        public ResponseDeleteMulti(IEnumerable<ResponseDeleteModel> data, string message)
            : base(message)
        {
            Data = data;
        }

        public ResponseDeleteMulti(string message)
            : base(message)
        {
        }

        public ResponseDeleteMulti()
        {
        }
    }
}
