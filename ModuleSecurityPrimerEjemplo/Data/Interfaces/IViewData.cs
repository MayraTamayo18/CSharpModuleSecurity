using Entity.Dto;
using Entity.Model.Security;

namespace Data.Interfaces
{
    public  interface IViewData
    {
        public Task Delete(int id);
        public Task<View> GetById(int id);
        public Task<View> Save(View entity);
        public Task Update(View entity);
        public Task<IEnumerable<ViewDto>> GetAll();
        public Task<IEnumerable<DataSelectDto>> GetAllSelect();


    }
}
