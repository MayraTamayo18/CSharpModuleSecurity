using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController: ControllerBase, IPersonController
    {
        private readonly IPersonaBusiness _PersonBusiness;

        public PersonController(IPersonaBusiness personBusiness)
        {
            _PersonBusiness = personBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var resul = await _PersonBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var result = await _PersonBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<PersonDto>> Save([FromBody] PersonDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _PersonBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] PersonDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _PersonBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _PersonBusiness.Delete(id);
            return NoContent();
        }
    }
}
