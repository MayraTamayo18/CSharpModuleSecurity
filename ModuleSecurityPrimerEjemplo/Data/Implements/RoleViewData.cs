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
    public class RoleViewData: IRoleViewData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public RoleViewData(ApplicationDbContext context, IConfiguration configuration)
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
            context.RoleViews.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<RoleViewDto>> GetAll()
        {
            var sql = @"SELECT 
                        rolw.Id,
                        rolw.State,
                        rolw.RoleId,
                        rolw.ViewId,
                        ro.Name AS Role,
                        vi.Name AS View
                    FROM roleviews AS rolw
                    INNER JOIN roles AS ro ON ro.Id=rolw.ViewId
                    INNER JOIN views AS vi ON vi.Id=rolw.RoleId
                    WHERE ISNULL(rolw.DeletedAt)";
            IEnumerable<RoleViewDto> roleViewDtos = await context.QueryAsync<RoleViewDto>(sql);
            return roleViewDtos;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Role_id, ' - ', View_id) AS TextoMostrar
                    FROM 
                      roleViews
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<RoleView> GetById(int id)
        {
            var sql = @"SELECT * FROM roleViews WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<RoleView>(sql, new { Id = id });
        }

        public async Task<RoleView> Save(RoleView entity)
        {
            context.RoleViews.Add(entity);
            await context.SaveChangesAsync();
            return entity; 
        }

        public async Task Update(RoleView entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

    }
}
