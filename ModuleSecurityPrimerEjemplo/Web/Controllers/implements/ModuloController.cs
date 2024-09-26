using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class ModuloController: ControllerBase, IModulocontroller
    {
        private readonly IModuloBusiness _ModuloBusiness; 

        public ModuloController(IModuloBusiness moduloBusiness)
        {
            _ModuloBusiness = moduloBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuloDto>>> GetAll()
        {
            var resul = await _ModuloBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuloDto>> GetById(int id)
        {
            var result = await _ModuloBusiness.GetById(id);
            if(result == null)
            {
                return NotFound(); 
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<ModuloDto>> Save([FromBody]ModuloDto entity)
        {
            if(entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result= await _ModuloBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]ModuloDto entity)
        {
            if(entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _ModuloBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ModuloBusiness.Delete(id);
            return NoContent();
        }


    }
}
