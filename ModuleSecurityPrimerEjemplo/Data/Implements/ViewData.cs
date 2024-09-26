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
    public class ViewData: IViewData
    {

        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public ViewData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Views.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewDto>> GetAll()
        {
            var sql = @"SELECT 
                        vi.Id,
                        vi.State,
                        vi.Name, 
                        vi.Description,
                        vi.Route,
                        vi.ModuloId,
                        mo.Name AS Modulo
                        FROM views AS vi
                        INNER JOIN modulos AS mo ON mo.Id=vi.ModuloId
                        WHERE ISNULL(vi.DeletedAt)";
            return await context.QueryAsync<ViewDto>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Name, ' - ',Descriptions, ' - ', Route' - ', ModuloId) AS TextoMostrar
                    FROM 
                      views
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<View> GetById(int id)
        {
            var sql = @"SELECT * FROM views WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<View>(sql, new { Id = id });
        }

        public async Task<View> Save(View entity)
        {
            context.Views.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(View entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<View> GetByName(string name)
        {
            return await this.context.Views.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

    }
}
