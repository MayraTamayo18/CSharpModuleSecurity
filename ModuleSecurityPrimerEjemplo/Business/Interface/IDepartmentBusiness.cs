using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IDepartmentBusiness
    {
        public Task<IEnumerable<DepartmentDto>> GetAll();
        public Task<DepartmentDto> GetById(int id);
        public Task<Department> Save(DepartmentDto departmentDto);
        public Task Update(DepartmentDto departmentDto);
        public Task Delete(int id);
    }
}
