using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase, IUserController
    {
        private readonly IUserBusiness _UserBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var resul = await _UserBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var result = await _UserBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> Save([FromBody] UserDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _UserBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _UserBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _UserBusiness.Delete(id);
            return NoContent();
        }
    }
}
