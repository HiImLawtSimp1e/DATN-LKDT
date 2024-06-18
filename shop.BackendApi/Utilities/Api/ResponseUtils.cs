
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using shop.Domain.EntitiesInterface;
using shop.BackendApi.Utilities.Api.Response.Model;
namespace shop.BackendApi.Utilities.Api
{
    public static class ResponseUtils
    {
        public static ActionResult TransformData(shop.BackendApi.Utilities.Api.Response.Model.Response data)
        {
            return new ObjectResult(data)
            {
                StatusCode = (int)data.Code
            };
        }

        public static ActionResult CreateUnauthenticatedResult()
        {
            return CreateUnauthenticatedResult(StatusCodeEnum.Unauthorized, StatusCodeEnum.Unauthorized.ToString());
        }

        public static ActionResult CreateUnauthenticatedResult(string message)
        {
            return CreateUnauthenticatedResult(StatusCodeEnum.Unauthorized, message);
        }

        public static ActionResult CreateUnauthenticatedResult(StatusCodeEnum code, string message)
        {
            return TransformData(CreateResponseError(code, message));
        }

        public static ActionResult CreateErrorResult(string message)
        {
            return CreateErrorResult(StatusCodeEnum.BadRequest, message);
        }

        public static ActionResult CreateErrorResult(StatusCodeEnum code, string message)
        {
            return TransformData(CreateResponseError(code, message));
        }

        public static ActionResult CreateErrorResult(ModelStateDictionary modelState)
        {
            return CreateErrorResult(StatusCodeEnum.BadRequest, modelState);
        }

        public static ActionResult CreateErrorResult(StatusCodeEnum code, ModelStateDictionary modelState)
        {
            List<DetailedErrorResponse> list = new List<DetailedErrorResponse>();
            foreach (string key in modelState.Keys)
            {
                foreach (ModelError error in modelState[key].Errors)
                {
                    list.Add(new DetailedErrorResponse(key, error.ErrorMessage));
                }
            }

            Dictionary<string, string> errorDetail = list.ToDictionary((DetailedErrorResponse x) => x.Key.ToString(), (DetailedErrorResponse x) => x.ErrorMessage);
            return CreateErrorResult(code, list?.FirstOrDefault().Key, errorDetail);
        }

        public static ActionResult CreateErrorResult(StatusCodeEnum code, string message, Dictionary<string, string> errorDetail)
        {
            return TransformData(CreateResponseError(code, message, new List<Dictionary<string, string>> { errorDetail }));
        }

        public static ActionResult CreateUpdateResult(Guid id)
        {
            return TransformData(CreateResponseUpdate(id));
        }

        public static ActionResult CreateUpdateResult(string name)
        {
            return TransformData(CreateResponseUpdate(name));
        }

        public static ActionResult CreateUpdateResult(Guid id, string name)
        {
            return TransformData(CreateResponseUpdate(id, name));
        }

        public static shop.BackendApi.Utilities.Api.Response.Model.Response CreateResponseUpdate<T>(T item)
        {
            try
            {
                return new ResponseObject<T>(item);
            }
            catch (Exception)
            {
                if ((object)item is IIdEntity idEntity)
                {
                    return new ResponseUpdate(idEntity.Id);
                }

                throw;
            }
        }

        public static ResponseUpdate CreateResponseUpdate(Guid id)
        {
            return new ResponseUpdate(id);
        }

        public static ResponseUpdateMulti CreateResponseUpdateMulti(IEnumerable<Guid> ids)
        {
            return new ResponseUpdateMulti(ids.Select((Guid x) => new ResponseUpdateModel(x)).ToList());
        }

        public static ResponseUpdate CreateResponseUpdate(Guid id, string name)
        {
            return new ResponseUpdate(new ResponseUpdateModel
            {
                Id = id,
                Name = name
            });
        }

        public static ResponseDelete CreateResponseDelete(string name)
        {
            return new ResponseDelete(name);
        }

        public static ResponseDelete CreateResponseDelete(Guid id, string name)
        {
            return new ResponseDelete(id, name);
        }

        public static ResponseDeleteModel CreateResponseDeleteModel<T>(T item)
        {
            ResponseDeleteModel responseDeleteModel = new ResponseDeleteModel();
            Guid guid = ((item is Guid) ? Guid.Parse(item.ToString()) : Guid.Empty);
            if (guid != Guid.Empty)
            {
                responseDeleteModel.Id = guid;
            }

            if ((object)item is IIdEntity idEntity)
            {
                responseDeleteModel.Id = idEntity.Id;
            }

            string text = item as string;
            if (!string.IsNullOrWhiteSpace(text))
            {
                responseDeleteModel.Name = text;
            }

            if ((object)item is IDescriptionEntity descriptionEntity)
            {
                responseDeleteModel.Name = descriptionEntity.Description;
            }

            if ((object)item is ITitleEntity titleEntity)
            {
                responseDeleteModel.Name = titleEntity.Title;
            }

            if ((object)item is INameEntity nameEntity)
            {
                responseDeleteModel.Name = nameEntity.Name;
            }

            if ((object)item is ICodeEntity codeEntity)
            {
                responseDeleteModel.Name = codeEntity.Code;
            }

            return responseDeleteModel;
        }

        public static ResponseDeleteMulti CreateResponseDeleteMulti(IEnumerable<Guid> items)
        {
            return new ResponseDeleteMulti
            {
                Data = items.Select((Guid x) => CreateResponseDeleteModel(x)).ToList()
            };
        }

        public static ResponseDeleteMulti CreateResponseDeleteMulti<T>(IEnumerable<T> items)
        {
            return new ResponseDeleteMulti
            {
                Data = items.Select((T x) => CreateResponseDeleteModel(x)).ToList()
            };
        }

        public static ResponseError CreateResponseError(string message)
        {
            return new ResponseError(StatusCodeEnum.BadRequest, message);
        }

        public static ResponseError CreateResponseError(StatusCodeEnum code, string message)
        {
            return new ResponseError(code, message);
        }

        public static ResponseError CreateResponseError(StatusCodeEnum code, string message, List<Dictionary<string, string>> errorDetail)
        {
            return new ResponseError(code, message, errorDetail);
        }


    }
}
