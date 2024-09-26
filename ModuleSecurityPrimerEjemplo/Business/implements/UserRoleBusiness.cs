using Business.Interface;
using Data.Interfaces;
using Entity.Dto;
using Entity.Model.Security;

namespace Business.implements
{
    public class UserRoleBusiness:IUserRoleBusiness
    {
        protected readonly IUserRoleData data;

        public UserRoleBusiness(IUserRoleData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<UserRoleDto>> GetAll()
        {
            IEnumerable<UserRoleDto> userRoles = await this.data.GetAll();

            //var UserRoleDtos = userRoles.Select(userRole => new UserRoleDto
            //{
            //    Id = userRole.Id,
            //    UserId = userRole.UserId,
            //    //User = userRole.User,
            //    RoleId = userRole.RoleId,
            //    //Role = userRole.Role,
            //    State = userRole.State
            //});

            return userRoles;
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<UserRoleDto> GetById(int id)
        {
            UserRole userRole = await this.data.GetById(id);
            if (userRole == null)
            {
                throw new Exception("Registro no encontrado");
            }

            UserRoleDto userDto = new UserRoleDto
            {
                Id = userRole.Id,
                UserId = userRole.UserId,
                //User= userRole.User,
                RoleId = userRole.RoleId,
                //Role = userRole.Role,
                State = userRole.State
            };

            return userDto;
        }

        public UserRole mapearDatos(UserRole userRole, UserRoleDto entity)
        {
            userRole.Id = entity.Id;
            userRole.UserId = entity.UserId;
           // userRole.User = entity.User;
            userRole.RoleId = entity.RoleId;
           // userRole.Role = entity.Role;
            userRole.State = entity.State;
            return userRole;
        }

        public async Task<UserRole> Save(UserRoleDto entity)
        {
            UserRole userRole = new UserRole
            {
                CreatedAt = DateTime.Now.AddHours(-5)
            };
            userRole = this.mapearDatos(userRole, entity);
            return await this.data.Save(userRole);
        }

        public async Task Update(UserRoleDto entity)
        {
            UserRole userRole = await this.data.GetById(entity.Id);
            if (userRole == null)
            {
                throw new Exception("Registro no encontrado");
            }
            userRole = this.mapearDatos(userRole, entity);
            await this.data.Update(userRole);
        }
    }
}
