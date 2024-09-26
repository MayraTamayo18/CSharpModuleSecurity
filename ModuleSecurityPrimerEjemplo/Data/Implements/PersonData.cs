using System;

//using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Dto;
using Entity.Model.Security;
using Data.Interfaces;

//donde se encuentra el archivo 
namespace Data.implements

	//aqui es donde se implementa el servicio como se va hacer aqui se definen metodos guardar.eliminar.actualizar,editar
{
	public class PersonData: IPersonData
	{
		// readonly: que el valor del context se agine solo una vez
		// ApplicationDBContext context; : se declara una variable llamada context de tipo ApplicationDBContext 

		private readonly ApplicationDbContext context;
		protected readonly IConfiguration configuration;

		public PersonData(ApplicationDbContext context, IConfiguration configuration)
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
			context.Person.Remove(entity);
			await context.SaveChangesAsync();
		}

        public async Task<IEnumerable<Person>> GetAll()
        {
            var sql = @"SELECT
                        *
                      FROM
                          person
                      ORDER BY Id ASC";
            return await context.QueryAsync<Person>(sql);
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
		{
			var sql = @"SELECT
                       Id,
                       CONCAT(First_name, ' - ', Last_name, ' - ', Email, ' - ', Phone, ' _ ', Addres, ' - ', Type_document, ' - ', Document) AS TextoMostrar
                    FROM 
                     person
                    WHERE DeletedAt IS NULL AND State= 1 
				    ORDER BY Id ASC";
			return await context.QueryAsync<DataSelectDto>(sql);
		}
		public async Task<Person>GetById(int id)
		{
			var sql = @"SELECT * FROM person WHERE Id = @Id ORDER BY Id ASC";
			return await this.context.QueryFirstOrDefaultAsync<Person>(sql, new { Id = id });
		}
		public async Task<Person> Save(Person entity)
		{
			context.Person.Add(entity);
			await context.SaveChangesAsync();
			return entity; 
		}
		public async Task Update(Person entity)
		{
			context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await context.SaveChangesAsync();
		}
	
	}

}

//mapear 