using Business.Interface;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.implements
{
    [ApiController]
    [Route("[controller]")]
    public class ViewController: ControllerBase, IViewController
    {
        private readonly IViewBusiness _viewBusiness;

        public ViewController(IViewBusiness viewBusiness)
        {
            _viewBusiness = viewBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDto>>> GetAll()
        {
            var resul = await _viewBusiness.GetAll();
            return Ok(resul);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDto>> GetById(int id)
        {
            var result = await _viewBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<ViewDto>> Save([FromBody] ViewDto entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null");
            }
            var result = await _viewBusiness.Save(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ViewDto entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest();
            }
            await _viewBusiness.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _viewBusiness.Delete(id);
            return NoContent();
        }
    }
}
