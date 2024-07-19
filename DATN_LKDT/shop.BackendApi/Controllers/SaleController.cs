using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Infrastructure.Repositories.Sale;
using shop.Domain.Entities;
namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {

            var a = await _saleRepository.GetAll();
            return Ok(a);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(SalesEntity obj)
        {
            var a = await _saleRepository.Add(obj);
            return Ok(a);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Put(SalesEntity obj)
        {
            var a = await _saleRepository.Update(obj);
            return Ok(a);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _saleRepository.Delete(id);
            return Ok();
        }

    }
}
