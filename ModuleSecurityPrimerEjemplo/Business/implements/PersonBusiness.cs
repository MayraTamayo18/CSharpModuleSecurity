using Business.Interface;
using Data.Interfaces;
using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Business.implements
{
    public class PersonBusiness: IPersonaBusiness
    {
        protected readonly IPersonData data;

        public PersonBusiness(IPersonData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            IEnumerable<Person> persons = await this.data.GetAll();

            var personDto = persons.Select(person => new PersonDto
            {
                Id = person.Id,
                First_name = person.Frist_name,
                last_name = person.Last_name,
                Email = person.Email,
                Phone =person.Phone,
                Type_document= person.Type_documment,
                Document= person.Document,
                Addres = person.Addres,
                State = person.State
            });

            return personDto;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<PersonDto> GetById(int id)
        {
            Person person = await this.data.GetById(id);
            if (person == null)
            {
                throw new Exception("Registro no encontrado");
            }
            PersonDto personDto = new PersonDto
            {
                Id = person.Id,
                First_name = person.Frist_name,
                last_name = person.Last_name,
                Email = person.Email,
                Phone = person.Phone,
                Type_document = person.Type_documment,
                Document = person.Document,
                Addres = person.Addres,
                State = person.State

            };
            return personDto;
        }

        public Person mapearDatos(Person person, PersonDto entity)
        {

            person.Id = entity.Id;
            person.Frist_name = entity.First_name;
            person.Last_name = entity.last_name; 
            person.Email = entity.Email;
            person.Phone = entity.Phone;
            person.Type_documment= entity.Type_document;
            person.Document= entity.Document;
            person.Addres = entity.Addres;
            person.State= entity.State;

            return person;
        }

        public async Task<Person> Save(PersonDto entity)
        {
            Person person = new Person
            {
                CreatedAt = DateTime.Now.AddHours(-5)
            };
            person = this.mapearDatos(person, entity);
            return await this.data.Save(person);
        }

        public async Task Update(PersonDto entity)
        {
            Person person = await this.data.GetById(entity.Id);
            if (person == null)
            {
                throw new Exception("Registro no encontrado");
            }
            person = this.mapearDatos(person, entity);
            await this.data.Update(person);
        }

    }
}
