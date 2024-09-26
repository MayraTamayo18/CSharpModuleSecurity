using Business.implements;
using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class CityController: ControllerBase, ICityController
    {
        private readonly ICityBusiness _CityBusiness;

        public CityController(ICityBusiness cityBusiness)
        {
            _CityBusiness = cityBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAll()
        {
            var resul = await _CityBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetById(int id)
        {
            var result = await _CityBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<CityDto>> Save([FromBody] CityDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _CityBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CityDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _CityBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _CityBusiness.Delete(id);
            return NoContent();
        }

    }
}
