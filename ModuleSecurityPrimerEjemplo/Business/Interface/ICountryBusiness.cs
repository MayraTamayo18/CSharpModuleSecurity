using Entity.Dto;
using Entity.Model.Security;


namespace Business.Interface
{
    public interface ICountryBusiness
    {
        public Task<IEnumerable<CountryDto>> GetAll();
        public Task<CountryDto> GetById(int id);
        public Task<Country> Save(CountryDto countryDto);
        public Task Update(CountryDto countryDto);
        public Task Delete(int id);
    }
}
