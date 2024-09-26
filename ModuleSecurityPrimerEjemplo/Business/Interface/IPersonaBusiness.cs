using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IPersonaBusiness
    {
        public Task Delete(int id);
        public Task<PersonDto> GetById(int id);
        public Task<IEnumerable<PersonDto>> GetAll();
        public Task<Person> Save(PersonDto entity);
        public Task Update(PersonDto entity);
    }
}
