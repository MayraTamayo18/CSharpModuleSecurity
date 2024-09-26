using Entity.Dto;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IModuloData
    {
        public Task Delete(int id);
        public Task<Modulo> Save(Modulo entity);
        public Task Update(Modulo entity);
        public Task<Modulo> GetById(int id);
        public Task<IEnumerable<Modulo>> GetAll();
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();

    }
}
