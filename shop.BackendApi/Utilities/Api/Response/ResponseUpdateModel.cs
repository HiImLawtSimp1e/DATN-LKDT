namespace shop.BackendApi.Utilities.Api.Response.Model
{
    public class ResponseUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ResponseUpdateModel()
        {
        }

        public ResponseUpdateModel(Guid id)
        {
            Id = id;
        }

        public ResponseUpdateModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
