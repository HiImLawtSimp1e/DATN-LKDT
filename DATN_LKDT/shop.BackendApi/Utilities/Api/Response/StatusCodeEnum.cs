namespace shop.BackendApi.Utilities.Api.Response.Model
{ 
public enum StatusCodeEnum
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        Unauthorized = 401,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503
    }
}
