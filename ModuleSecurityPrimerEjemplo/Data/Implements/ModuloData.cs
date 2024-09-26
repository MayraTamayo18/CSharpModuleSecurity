using Data.Interfaces;
using Entity.Context;
using Entity.Dto;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.implements
{
    public class ModuloData : IModuloData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public ModuloData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Modulos.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Modulo>> GetAll()
        {
            var sql = @"SELECT
                        *
                      FROM
                          modulos
                      ORDER BY Id ASC";
            return await context.QueryAsync<Modulo>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Name, ' - ',Descriptions) AS TextoMostrar
                    FROM 
                      modulos
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<Modulo> GetById(int id)
        {
            var sql = @"SELECT * FROM modulos WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Modulo>(sql, new { Id = id });
        }

        public async Task<Modulo> GetByName(string name)
        {
            return await this.context.Modulos.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Modulo> Save(Modulo entity)
        {
            context.Modulos.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Modulo entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

        }

    }  
}
