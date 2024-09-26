using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController: ControllerBase, IUserRoleController
    {
        private readonly IUserRoleBusiness _UserRoleBusiness; 
        
        public UserRoleController(IUserRoleBusiness userRoleBusiness)
        {
            _UserRoleBusiness = userRoleBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll()
        {
            var resul = await _UserRoleBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleDto>> GetById(int id)
        {
            var result = await _UserRoleBusiness.GetById(id);
            if(result == null)
            {
                return NotFound(); 
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<UserRoleDto>> Save([FromBody]UserRoleDto entity)
        {
            if(entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result= await _UserRoleBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);  
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]UserRoleDto entity)
        {
            if(entity == null || entity.Id==0)
            {
                return BadRequest(); 
            }
            await _UserRoleBusiness.Update(entity);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _UserRoleBusiness.Delete(id);
            return NoContent(); 
        }

    }
}
