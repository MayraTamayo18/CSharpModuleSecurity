using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICountryData
    {
        public Task<IEnumerable<Country>> GetAll();
        public Task<Country> GetById(int id);
        public Task<Country> Save(Country country);
        public Task Update(Country country);
        public Task Delete(int id);
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();
    }
}
