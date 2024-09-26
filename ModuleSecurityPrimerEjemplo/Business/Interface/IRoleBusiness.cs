using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IRoleBusiness
    {
        public Task Delete(int id);
        public Task<RoleDto> GetById(int id);
        public Task<IEnumerable<RoleDto>> GetAll();
        public Task<Role> Save(RoleDto entity);
        public Task Update(RoleDto entity);
    }
}
