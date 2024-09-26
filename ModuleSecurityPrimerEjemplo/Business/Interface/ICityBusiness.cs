using Entity.Dto;
using Entity.Model.Security;

namespace Business.Interface
{
    public interface ICityBusiness
    {
       public Task<IEnumerable<CityDto>> GetAll();
       public Task<CityDto> GetById(int id);
       public Task<City> Save(CityDto cityDto);
       public Task Update(CityDto cityDto);
       public Task Delete(int id);
    }
}
