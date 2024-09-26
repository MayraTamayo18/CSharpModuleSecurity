using Business.Interface;
using Data.Interfaces;
using Entity.Dto;
using Entity.Model.Security;


namespace Business.implements
{
    public  class RoleViewBusiness: IRoleViewBusiness
    {
        protected readonly IRoleViewData data;

        public RoleViewBusiness(IRoleViewData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<RoleViewDto>> GetAll()
        {
            IEnumerable<RoleViewDto> roleViews = await this.data.GetAll();

            //var roleViewDtos = roleViews.Select(roleView => new RoleViewDto
            //{
            //    Id = roleView.Id,
            //    RoleId = roleView.RoleId,
            //    ViewId = roleView.ViewId,
            //    //role= roleView.role,
            //    //view = roleView.view,
            //    State = roleView.State
            //});

            return roleViews;
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<RoleViewDto> GetById(int id)
        {
            RoleView roleView = await this.data.GetById(id);
            if (roleView == null)
            {
                throw new Exception("Registro no encontrado");
            }

            RoleViewDto roleViewDto = new RoleViewDto
            {
                Id = roleView.Id,
                RoleId = roleView.RoleId,
                ViewId = roleView.ViewId,
                //role = roleView.role,
                //view = roleView.view,
                State = roleView.State
            };

            return roleViewDto;
        }

        public RoleView mapearDatos(RoleView  roleView, RoleViewDto entity)
        {
            roleView.Id = entity.Id;
            roleView.RoleId = entity.RoleId;
            roleView.ViewId = entity.ViewId;
            //roleView.role = entity.role;
            //roleView.view = entity.view;
            roleView.State = entity.State;

            return roleView;
        }

        public async Task<RoleView> Save(RoleViewDto entity)
        {
            RoleView roleView = new RoleView
            {
                CreatedAt = DateTime.Now.AddHours(-5)
            };
            roleView = this.mapearDatos(roleView, entity);
            //roleView.Role = null;
            return await this.data.Save(roleView);
        }

        public async Task Update(RoleViewDto entity)
        {
            RoleView roleView = await this.data.GetById(entity.Id);
            if (roleView == null)
            {
                throw new Exception("Registro no encontrado");
            }
            roleView = this.mapearDatos(roleView, entity);
            await this.data.Update(roleView);
        }
    }
}
