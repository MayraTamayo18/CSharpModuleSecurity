using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IUserController
    {
        public Task<ActionResult<IEnumerable<UserDto>>> GetAll();
        public Task<ActionResult<UserDto>> Save([FromBody] UserDto userDto);
        public Task<IActionResult> Update([FromBody] UserDto userDto);
        public Task<IActionResult> Delete(int id);
    }
}
