using AppData.IRepositories;

using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using shop.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailController : ControllerBase
    {
        private IBillDetailRepository _billDetailRepository;
        public BillDetailController(IBillDetailRepository billDetailRepository)
        {
            _billDetailRepository = billDetailRepository;
        }
        [HttpGet]

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {

            var a = await _billDetailRepository.GetAll();
            return Ok(a);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var a = await _billDetailRepository.GetById(id);
            return Ok(a);
        }

        // POST api/<BillController>
        [HttpPost("add")]
        public async Task<IActionResult> Post(BillDetailsEntity obj)
        {
            var a = await _billDetailRepository.Add(obj);
            return Ok(a);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(BillDetailsEntity obj)
        {
            var a = await _billDetailRepository.Update(obj);
            return Ok(a);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _billDetailRepository.Delete(id);
            return Ok();
        }
    }
}
