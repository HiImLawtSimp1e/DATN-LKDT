namespace shop.Infrastructure.Model.Common;

public class SelectedItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Selected { get; set; }
    public object Select()
    {
        throw new NotImplementedException();
    }
}