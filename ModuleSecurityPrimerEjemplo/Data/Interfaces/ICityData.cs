using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICityData
    {
        public Task<IEnumerable<CityDto>> GetAll();
        public Task<City> GetById(int id);
        public Task<City> Save(City city);
        public Task Update(City city);
        public Task Delete(int id);
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();
    }
}
