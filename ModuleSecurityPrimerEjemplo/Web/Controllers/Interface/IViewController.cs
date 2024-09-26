using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IViewController
    {
        public Task<ActionResult<IEnumerable<ViewDto>>> GetAll();
        public Task<ActionResult<ViewDto>> Save([FromBody] ViewDto viewDto);
        public Task<IActionResult> Update([FromBody] ViewDto viewDto);
        public Task<IActionResult> Delete(int id);
    }
}
