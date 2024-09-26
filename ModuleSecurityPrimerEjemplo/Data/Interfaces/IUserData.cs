using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUserData
    {
        public Task Delete(int id);
        public Task<User> GetById(int Id);
        public Task<User> Save(User entity);
        public Task Update(User entity);
        public Task<IEnumerable<UserDto>> GetAll();
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();

    }
}
