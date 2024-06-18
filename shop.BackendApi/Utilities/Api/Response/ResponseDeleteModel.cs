namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseDeleteModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ResponseDeleteModel()
        {
        }

        public ResponseDeleteModel(Guid id)
        {
            Id = id;
        }
    }
}
