using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using shop.Domain.Entities;
using shop.Infrastructure.Repositories.Bill;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private IBillRepository _billRepository;
        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        [HttpGet]

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {

            var a = await _billRepository.GetAll();
            return Ok(a);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var a = await _billRepository.GetById(id);
            return Ok(a);
        }

        // POST api/<BillController>
        [HttpPost("add")]
        public async Task<IActionResult> Post(BillEntity obj)
        {
            var a = await _billRepository.Add(obj);
            return Ok(a);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(BillEntity obj)
        {
            var a = await _billRepository.Update(obj);
            return Ok(a);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _billRepository.Delete(id);
            return Ok();
        }
    }
}
