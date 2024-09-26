using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IRoleController
    {
        public Task<ActionResult<IEnumerable<RoleDto>>> GetAll();
        public Task<ActionResult<RoleDto>> Save([FromBody] RoleDto roleDto);
        public Task<IActionResult> Update([FromBody] RoleDto roleDto);
        public Task<IActionResult> Delete(int id);
    }
}
