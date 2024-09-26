using Business.Interface;
using Data.Interfaces;
using Entity.Dto;
using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.implements
{
    public class DepartmentBusiness : IDepartmentBusiness
    {
        protected readonly IDepartmentData data;

        public DepartmentBusiness(IDepartmentData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<DepartmentDto>> GetAll()
        {
            IEnumerable<DepartmentDto> departments = await this.data.GetAll();

            //var departmentDtos = department.Select(department => new DepartmentDto
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    CountryId = department.CountryId,
            //    State = department.State
            //});

            return departments;
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }
        public async Task<DepartmentDto> GetById(int id)
        {
            Department department = await this.data.GetById(id);
            if (department == null)
            {
                throw new Exception("Registro no encontrado");
            }
            DepartmentDto departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CountryId = department.CountryId,

            };
            return departmentDto;
        }

        public Department mapearDatos(Department department, DepartmentDto entity)
        {
            department.Id = entity.Id;
            department.Name = entity.Name;
            department.Code = entity.Code;
            department.Description = entity.Description;
            department.CountryId = entity.CountryId;
            department.State = entity.State;
            return department;
        }

        public async Task<Department> Save(DepartmentDto entity)
        {
            Department department = new Department
            {
                CreatedAt = DateTime.Now.AddHours(-5)
            };
            department = this.mapearDatos(department, entity);
            return await this.data.Save(department);
        }

        public async Task Update(DepartmentDto entity)
        {
            Department department = await this.data.GetById(entity.Id);
            if (department == null)
            {
                throw new Exception("Registro no encontrado");
            }
            department = this.mapearDatos(department, entity);
            await this.data.Update(department);
        }
    }
}
