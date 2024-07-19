namespace shop.BackendApi.Utilities.Api
{
    public readonly struct DetailedErrorResponse
    {
        public DetailedErrorResponse(string x, string y)
        {
            Key = x;
            ErrorMessage = y;
        }
        public string Key { get; }
        public string ErrorMessage { get; }
    }
}