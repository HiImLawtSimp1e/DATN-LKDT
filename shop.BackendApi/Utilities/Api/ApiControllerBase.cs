using Azure;
using Microsoft.AspNetCore.Mvc;
using shop.BackendApi.Utilities.Api.Response;
using shop.BackendApi.Utilities.Api.Response.Model;
using shop.Infrastructure.Model.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace shop.BackendApi.Utilities.Api
{
    public class ApiControllerBase: ControllerBase
    {


        protected readonly ILogger<ApiControllerBase> _logger;

        public ApiControllerBase( ILogger<ApiControllerBase> logger)
        {
            _logger = logger;
        }
        public virtual async Task<IActionResult> ExecuteFunction<T>(Func<Task<T>> func)
        {
            try
            {
                return ParseResult(await func());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, string.Empty);
                return Content(exception.Message);
            }
        }
        private IActionResult ParseResult<T>(T result)
        {
            if (result == null)
            {
                return ResponseUtils.TransformData(new ResponseObject<T>(default(T), "null"));
            }

            if ((object)result is FileResult result2)
            {
                return result2;
            }

            if ((object)result is FileContentResult result3)
            {
                return result3;
            }

            if (result is Pagination<T>)
            {
                return ResponseUtils.TransformData(new ResponsePagination<T>(result as Pagination<T>));
            }

            if ((object)result is IActionResult result4)
            {
                return result4;
            }

            if ((object)result is  shop.BackendApi.Utilities.Api.Response.Model.Response data)
            {
                return ResponseUtils.TransformData(data);
            }

            return ResponseUtils.TransformData(new ResponseObject<T>(result));
        }

      

    }
}
