using EnMasseWebService.Models.Entities;
using EnMasseWebService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnMasseWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly CafeService _cafeService;
        public CafeController(CafeService cafeService)
        {
            _cafeService = cafeService;
        }

        [HttpGet]
        public async Task<List<Cafe>> GetAllCafes()
        {
            var cafeList = await _cafeService.GetAllCafesAsync();
            return cafeList;
        }

        /**
        // GET api/<CafeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CafeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CafeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CafeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
