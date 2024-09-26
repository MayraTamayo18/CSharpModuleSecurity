using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserBusiness
    {
        public Task Delete(int id);
        public Task<UserDto> GetById(int id);
        public Task<IEnumerable<UserDto>> GetAll();
        public Task<User> Save(UserDto entity);
        public Task Update(UserDto entity);
    }
}
