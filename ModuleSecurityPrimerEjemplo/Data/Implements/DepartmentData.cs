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
    public class DepartmentData:IDepartmentData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public DepartmentData(ApplicationDbContext context, IConfiguration configuration)
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
            context.departments.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<DepartmentDto>> GetAll()
        {
            var sql = @"SELECT
                            de.Id,
                            de.State,
                            de.Name,
                            de.Code,
                            de.Description,
                            de.CountryId,
                            co.Name AS Country
                        FROM departments AS de
                        INNER JOIN countrys AS co ON co.Id=de.CountryId
                        WHERE ISNULL(de.DeletedAt)";
            return await context.QueryAsync<DepartmentDto>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Name, ' - ',Code, ' - ', Descriptions, ' - ',CountryId) AS TextoMostrar
                    FROM 
                      departments
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<Department> GetById(int id)
        {
            var sql = @"SELECT * FROM departments WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Department>(sql, new { Id = id });
        }

        public async Task<Department> GetByName(string name)
        {
            return await this.context.departments.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Department> Save(Department entity)
        {
            context.departments.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Department entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

        }
    }
}
