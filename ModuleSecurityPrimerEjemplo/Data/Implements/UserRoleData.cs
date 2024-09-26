using Data.Interfaces;
using Entity.Context;
using Entity.Dto;
using Entity.Model.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.implements
{
    public class UserRoleData:IUserRoleData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public UserRoleData(ApplicationDbContext context, IConfiguration configuration)
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
            context.UserRoles.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserRoleDto>> GetAll()
        {
            var sql= @"SELECT 
                        ur.Id,
                        ur.State,
                        ur.UserId,
                        ur.RoleId,
                        us.UserName AS User,
                        ro.Name AS Role
                    FROM userroles AS ur
                    INNER JOIN users AS us ON us.Id=ur.UserId
                    INNER JOIN roles AS ro ON ro.Id=ur.RoleId
                    WHERE ISNULL(ur.DeletedAt)";
            return await context.QueryAsync<UserRoleDto>(sql);
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(RoleId, ' - ', UserId) AS TextoMostrar
                    FROM 
                      userroles
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<UserRole> GetById(int id)
        {
            var sql = @"SELECT * FROM userroles WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<UserRole>(sql, new { Id = id });
        }
        public async Task<UserRole> Save(UserRole entity)
        {
            context.UserRoles.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(UserRole entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync(); 
        }
    }
}
