using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IRoleViewController
    {
        public Task<ActionResult<IEnumerable<RoleViewDto>>> GetAll(); 
        public Task<ActionResult<RoleViewDto>> Save([FromBody] RoleViewDto roleViewDto);
        public Task<IActionResult> Update([FromBody] RoleViewDto roleViewDto);
        public Task<IActionResult> Delete(int id);
    }
    
}
