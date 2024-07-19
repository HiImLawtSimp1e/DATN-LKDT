using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shop.BackendApi.Utilities.Api;
using shop.Domain.Entities;
using shop.Infrastructure.Business.Discount;
using shop.Infrastructure.Business.VirtualItem;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ApiControllerBase
    {
        private readonly IDiscountBusiness _discountBusiness;
        public DiscountController(IDiscountBusiness discountBusiness,ILogger<ApiControllerBase> logger) : base(logger)
        {
            _discountBusiness = discountBusiness;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountEntity>> Get(Guid id)
        {
            var result = await _discountBusiness.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<DiscountEntity>>> GetAll([FromQuery] DiscountQueryModel queryModel)
        {
            var result = await _discountBusiness.GetAllAsync(queryModel);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DiscountEntity>> Create(DiscountEntity discountEntity)
        {
            var result = await _discountBusiness.SaveAsync(discountEntity);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DiscountEntity discountEntity)
        {
            if (id != discountEntity.Id)
            {
                return BadRequest();
            }

            await _discountBusiness.SaveAsync(discountEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _discountBusiness.DeleteAsync(id);
            return NoContent();
        }
    }
}
