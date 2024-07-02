using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using shop.Domain.Entities;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // GET: api/<BlogController>
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var a = await _blogRepository.GetAll();
            return Ok(a);
        }

        // GET api/<BlogController>/5
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var a = await _blogRepository.GetById(id);
            return Ok(a);
        }

        // POST api/<BlogController>
        [HttpPost("add")]
        public async Task<IActionResult> Post(BlogEntity obj)
        {
            var a = await _blogRepository.Add(obj);
            return Ok(a);
        }

        // PUT api/<BlogController>/5
        [HttpPut("update")]
        public async Task<IActionResult> Put(BlogEntity obj)
        {
            var a = await _blogRepository.Update(obj);
            return Ok(a);
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _blogRepository.Delete(id);
            return Ok();
        }
    }
}
