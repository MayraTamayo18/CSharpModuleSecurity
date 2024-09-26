using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController: ControllerBase, IDepartmentController
    {
        private readonly IDepartmentBusiness _DepartmentBusiness;

        public DepartmentController(IDepartmentBusiness departmentBusiness)
        {
            _DepartmentBusiness = departmentBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var resul = await _DepartmentBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var result = await _DepartmentBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Save([FromBody] DepartmentDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _DepartmentBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] DepartmentDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _DepartmentBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _DepartmentBusiness.Delete(id);
            return NoContent();
        }
    }
}
