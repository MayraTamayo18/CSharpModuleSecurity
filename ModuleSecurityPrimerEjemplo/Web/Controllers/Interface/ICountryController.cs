using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface ICountryController
    {
        public Task<ActionResult<IEnumerable<CountryDto>>> GetAll();
        public Task<ActionResult<CountryDto>> GetById(int id);
        public Task<ActionResult<CountryDto>> Save([FromBody] CountryDto countryDto);
        public Task<IActionResult> Update([FromBody] CountryDto countryDto);
        public Task<IActionResult> Delete(int id);
    }
}
