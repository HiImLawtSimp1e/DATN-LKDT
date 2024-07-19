using shop.Infrastructure.Model.Common.Pagination;
using System.Text.Json.Serialization;

namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponsePagination<T> : Response
    {
        public Pagination<T> Data { get; set; }

        [JsonConstructor]
        public ResponsePagination(Pagination<T> data)
        {
            Data = data;
        }
    }
}
