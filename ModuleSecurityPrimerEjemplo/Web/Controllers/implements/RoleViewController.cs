using Business.implements;
using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class RoleViewController: ControllerBase, IRoleViewController
    {

        private readonly IRoleViewBusiness _RoleViewBusiness;

        public RoleViewController(IRoleViewBusiness roleViewBusiness)
        {
            _RoleViewBusiness = roleViewBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewDto>>> GetAll()
        {
            var resul = await _RoleViewBusiness.GetAll();
            return Ok(resul);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleViewDto>> GetById(int id)
        {
            var result = await _RoleViewBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RoleViewDto>> Save([FromBody] RoleViewDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _RoleViewBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleViewDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _RoleViewBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _RoleViewBusiness.Delete(id);
            return NoContent();
        }
    }
}
