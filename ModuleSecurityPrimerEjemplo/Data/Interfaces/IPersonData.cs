using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
    //aqui muestra lo que se va a hacer mas no como se va a hacer
{
    public interface IPersonData
    {
        public Task Delete(int id);
        public Task<Person> Save(Person entity);
        public Task Update(Person entity);
        
        public Task<Person> GetById(int id);
        public Task<IEnumerable<Person>> GetAll();
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();
    }
}
