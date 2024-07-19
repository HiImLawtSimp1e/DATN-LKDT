using Microsoft.AspNetCore.Mvc;
using shop.BackendApi.Utilities.Api;
using shop.Domain.Entities;
using shop.Infrastructure.Business.Contact;
using shop.Infrastructure.Business.Discount;
using shop.Infrastructure.Business.VirtualItem;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiControllerBase
    {
        private readonly IContactBusiness _contactBusiness;
        public ContactController(IContactBusiness contactBusiness, ILogger<ApiControllerBase> logger) : base(logger)
        {
            _contactBusiness = contactBusiness;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactEntity>> Get(Guid id)
        {
            var result = await _contactBusiness.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ContactEntity>>> GetAll([FromQuery] ContactQueryModel queryModel)
        {
            var result = await _contactBusiness.GetAllAsync(queryModel);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ContactEntity>> Create(ContactEntity contactEntity)
        {
            var result = await _contactBusiness.SaveAsync(contactEntity);
            return CreatedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ContactEntity contactEntity)
        {
            if (id != contactEntity.ID)
            {
                return BadRequest();
            }

            await _contactBusiness.SaveAsync(contactEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contactBusiness.DeleteAsync(id);
            return NoContent();
        }
    }
}
