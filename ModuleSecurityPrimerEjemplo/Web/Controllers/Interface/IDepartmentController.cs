using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IDepartmentController
    {
        public Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll();
        public Task<ActionResult<DepartmentDto>> GetById(int id);
        public Task<ActionResult<DepartmentDto>> Save([FromBody] DepartmentDto departmentDto);
        public Task<IActionResult> Update([FromBody] DepartmentDto departmentDto);
        public Task<IActionResult> Delete(int id);
    }
}
