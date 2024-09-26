using Data.Interfaces;
using Entity.Context;
using Entity.Dto;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Data.implements
{
    public class CountryData: ICountryData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public CountryData(ApplicationDbContext context, IConfiguration configuration)
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
            context.countrys.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Country>> GetAll()
        {
            var sql = @"SELECT
                        *
                      FROM
                         countrys
                     ORDER BY Id ASC";
            return await context.QueryAsync<Country>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(Name) AS TextoMostrar
                    FROM 
                      countrys
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<Country> GetById(int id)
        {
            var sql = @"SELECT * FROM countrys WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Country>(sql, new { Id = id });
        }

        public async Task<Country> GetByName(string name)
        {
            return await this.context.countrys.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Country> Save(Country entity)
        {
            context.countrys.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Country entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

        }
    }
}
