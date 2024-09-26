using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IUserRoleController
    {
        public Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll();
        public Task<ActionResult<UserRoleDto>> Save([FromBody] UserRoleDto userRoleDto);
        public Task<IActionResult> Update([FromBody] UserRoleDto userRoleDto);
        public Task<IActionResult>Delete(int id);
    }
}
