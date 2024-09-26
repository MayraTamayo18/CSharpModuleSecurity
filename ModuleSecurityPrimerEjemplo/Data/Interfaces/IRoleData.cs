using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRoleData
    {
        public Task Delete(int id);
        public Task<Role> GetById(int id);
        public Task<Role> Save(Role entity);
        public Task Update(Role entity);
        public Task<IEnumerable<Role>> GetAll();
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();
    }
     
    
}
