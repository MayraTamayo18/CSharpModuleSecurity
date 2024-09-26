using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController: ControllerBase, ICountryController
    {
        private readonly ICountryBusiness _CountryBusiness;

        public CountryController(ICountryBusiness countryBusiness)
        {
            _CountryBusiness = countryBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            var result = await _CountryBusiness.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var result = await _CountryBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<CountryDto>> Save([FromBody] CountryDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _CountryBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CountryDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _CountryBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _CountryBusiness.Delete(id);
            return NoContent();
        }
    }
}
