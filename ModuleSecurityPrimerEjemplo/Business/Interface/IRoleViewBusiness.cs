using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IRoleViewBusiness
    {
        public Task Delete(int id);
        public Task<RoleViewDto> GetById(int id);
        public Task<IEnumerable<RoleViewDto>> GetAll();
        public Task<RoleView> Save(RoleViewDto entity);
        public Task Update(RoleViewDto entity);
    }
}
