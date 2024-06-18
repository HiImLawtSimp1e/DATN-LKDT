using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Infrastructure.Business.VirtualItem;

namespace shop.BackendApi.Controllers
{
  
    [ApiController]
    [Route("api/virtualItem")]
    public class VirtualItemController : ControllerBase
    {
        private readonly IVirtualItemBusiness _virtualItemBusiness;

        public VirtualItemController(IVirtualItemBusiness virtualItemBusiness)
        {
            _virtualItemBusiness=virtualItemBusiness;
        }
    }
}
