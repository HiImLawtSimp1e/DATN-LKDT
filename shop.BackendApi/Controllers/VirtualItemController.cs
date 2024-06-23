using Microsoft.AspNetCore.Mvc;
using shop.BackendApi.Utilities.Api;
using shop.Domain.Entities;
using shop.Infrastructure.Business.VirtualItem;
using shop.Infrastructure.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualItemController : ApiControllerBase
    {
        private readonly IVirtualItemBusiness _virtualItemBusiness;

        public VirtualItemController(IVirtualItemBusiness virtualItemBusiness,ILogger<ApiControllerBase> logger) : base(logger)
        {
            _virtualItemBusiness = virtualItemBusiness;
        }


        // GET: api/<VirtualItemController>
        [HttpPost("filter")]
        public async Task<IActionResult> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel)
        {
            return await ExecuteFunction(async () =>
            {
                var item = await _virtualItemBusiness.GetAllAsync(virtualItemQueryModel);
                return item;
            });
        }

        // GET api/<VirtualItemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            return await ExecuteFunction(async () =>
            {
                var item = await _virtualItemBusiness.FindAsync(id);
                return item;
            });
        }

        // POST api/<VirtualItemController>
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] VirtualItemEntity virtualItemEntity)
        {
            return await ExecuteFunction(async () =>
            {
                var res = await _virtualItemBusiness.SaveAsync(virtualItemEntity);
                return res;
            });
    
        }

        // PUT api/<VirtualItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VirtualItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
