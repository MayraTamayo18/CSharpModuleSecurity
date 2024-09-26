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
    public class UserData:IUserData
    {
        private readonly ApplicationDbContext context;
        protected readonly IConfiguration configuration;

        public UserData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Users.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var sql = @"SELECT 
                        us.Id,
                        us.State,
                        us.UserName,
                        us.Password, 
                        us.personId,
                        pe.Frist_name AS person
                       FROM users AS us
                       INNER JOIN person AS pe ON pe.Id=us.personId
                       WHERE ISNULL(us.DeletedAt)";
            return await context.QueryAsync<UserDto>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT
                       Id,
                       CONCAT(UserName, ' - ' Password' - ',personId) AS TextoMostrar
                    FROM 
                      users
                    ORDER BY Id ASC";
            return await context.QueryAsync<DataSelectDto>(sql);
        }
        public async Task<User> GetById(int id)
        {
            var sql = @"SELECT * FROM users WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }
        public async Task<User> Save(User entity)
        {
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task Update(User entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync(); 
        }
        public async Task<User> GetByUsername(string username)
        {
            return await this.context.Users.AsNoTracking().Where(item => item.UserName == username).FirstOrDefaultAsync();
        }
        public async Task<User> GetByPassword(string password)
        {
            return await this.context.Users.AsNoTracking().Where(item => item.Password == password).FirstOrDefaultAsync();
        }
    }
}
