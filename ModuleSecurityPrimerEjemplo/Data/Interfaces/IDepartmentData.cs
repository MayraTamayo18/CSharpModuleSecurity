using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDepartmentData
    {
        public Task<IEnumerable<DepartmentDto>> GetAll();
        public Task<Department> GetById(int id);
        public Task<Department> Save(Department department);
        public Task Update(Department department);
        public Task Delete(int id);
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();

    }
}
