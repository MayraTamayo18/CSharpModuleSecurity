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
    public class CityData:ICityData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public CityData(ApplicationDbContext context, IConfiguration configuration)
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
            context.citys.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CityDto>> GetAll()
        {
            var sql = @"SELECT
                        ci. Id,
                        ci.State,
                        ci.Name,
                        ci.Code,
                        ci.Description,
                        ci.DepartmentId,
                        de.Name AS Department

                      FROM citys AS ci
                      INNER JOIN departments AS de ON de.Id=ci.DepartmentId
                      WHERE ISNULL(ci.DeletedAt)";
            return await context.QueryAsync<CityDto>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Name, ' - ',Code, ' - ', Description, ' - ',DepartmentId) AS TextoMostrar
                    FROM 
                      citys
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<City> GetById(int id)
        {
            var sql = @"SELECT * FROM citys WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<City>(sql, new { Id = id });
        }

        public async Task<City> GetByName(string name)
        {
            return await this.context.citys.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<City> Save(City entity)
        {
            context.citys.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(City entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

        }

  
    }
}
