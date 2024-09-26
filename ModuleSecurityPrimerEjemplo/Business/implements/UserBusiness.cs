using Business.Interface;
using Data.Interfaces;
using Entity.Dto;
using Entity.Model.Security;

namespace Business.implements
{
    public class UserBusiness: IUserBusiness
    {
        protected readonly IUserData data;

        public UserBusiness(IUserData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            IEnumerable<UserDto> users = await this.data.GetAll();

            //var userDtos = users.Select(user => new UserDto
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    Password = user.Password,
            //    personId = user.personId,
            //    State = user.State
            //});

            return users;
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<UserDto> GetById(int id)
        {
            User user = await this.data.GetById(id);
            if (user == null)
            {
                throw new Exception("Registro no encontrado");
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                personId = user.personId,
                State = user.State
            };

            return userDto;
        }

        public User mapearDatos(User user, UserDto entity)
        {
            user.Id = entity.Id;
            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.personId = entity.personId;
            user.State = entity.State;
            return user;
        }

        public async Task<User> Save(UserDto entity)
        {
            User user = new User
            {
                CreatedAt = DateTime.Now.AddHours(-5)
            };
            user = this.mapearDatos(user, entity);
            return await this.data.Save(user);
        }

        public async Task Update(UserDto entity)
        {
            User user = await this.data.GetById(entity.Id);
            if (user == null)
            {
                throw new Exception("Registro no encontrado");
            }
            user = this.mapearDatos(user, entity);
            await this.data.Update(user);
        }
    }
}
