using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface ICityController
    {
        public Task<ActionResult<IEnumerable<CityDto>>> GetAll();
        public Task<ActionResult<CityDto>> GetById(int id);
        public Task<ActionResult<CityDto>> Save([FromBody]CityDto cityDto);
        public Task<IActionResult> Update([FromBody] CityDto cityDto);
        public Task<IActionResult> Delete(int id);
    }
}
