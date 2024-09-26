using Data.Interfaces;
using Entity.Context;
using Entity.Dto;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.implements
{
    public class RoleData: IRoleData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public RoleData(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new Exception("el Registro no se encontro");
            }
            entity.DeletedAt = DateTime.Parse(DateTime.Today.ToString());
            context.Roles.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Role>>GetAll()
        {
            var sql = @"SELECT
                         *
                      FROM
                         roles 
				    ORDER BY Id ASC";
            return await context.QueryAsync<Role>(sql);
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Name, ' - ', Description) AS TextoMostrar
                    FROM 
                     roles
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<Role> GetById(int id)
        {
            var sql = @"SELECT * FROM roles WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Role>(sql, new { Id = id });
        }
        public async Task<Role> Save(Role entity)
        {
            context.Roles.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Role entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Role> GetByName(string name)
        {
            return await this.context.Roles.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }
    }
}
