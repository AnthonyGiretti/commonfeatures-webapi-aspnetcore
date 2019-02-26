using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiDemo.Models;
using WebApiDemo.Repositories;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoCRUDController : ControllerBase
    {
        private ICountryRepository _countryRepository;

        public DemoCRUDController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Get()
        {
            return await _countryRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);

            if (null == country)
                return NotFound();

            return Ok(country);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(Country country)
        {
            var id = await _countryRepository.AddAsync(country);
            return Created($"http://localhost:56190/api/DemoCRUD/{id}", id);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id, Country country)
        {
            var affectedItem = await _countryRepository.UpdateAsync(id, country);
            if (affectedItem > 0)
                return NoContent();

            return UnprocessableEntity();
        }

        [HttpPatch("update/{id}/description")]
        public async Task<IActionResult> Patch(int id, [FromBody]string description)
        {
            var affectedItem = await _countryRepository.UpdateDescriptionAsync(id, description);
            if (affectedItem > 0)
                return NoContent();

            return UnprocessableEntity();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedItem = await _countryRepository.DeleteAsync(id);
            if (affectedItem > 0)
                return NoContent();

            return UnprocessableEntity();
        }
    }
}