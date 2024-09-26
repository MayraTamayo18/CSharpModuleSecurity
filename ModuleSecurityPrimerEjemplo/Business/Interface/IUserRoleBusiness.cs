using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserRoleBusiness
    {
        public Task Delete(int id);
        public Task<UserRoleDto> GetById(int id);
        public Task<IEnumerable<UserRoleDto>> GetAll();
        public Task<UserRole> Save(UserRoleDto entity);
        public Task Update(UserRoleDto entity);
    }
}
