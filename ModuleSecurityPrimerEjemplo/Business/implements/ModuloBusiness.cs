using Business.Interface;
using Data.Interfaces;
using Entity.Dto;
using Entity.Model.Security;


namespace Business.implements
{
    public class ModuloBusiness: IModuloBusiness
    {
        protected readonly IModuloData data; 
        
        public ModuloBusiness(IModuloData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<ModuloDto>> GetAll()
        {
            IEnumerable<Modulo> modulos = await this.data.GetAll();

            var moduloDtos = modulos.Select(modulo => new ModuloDto
            {
                Id = modulo.Id,
                Name = modulo.Name,
                Description = modulo.Description,
                Route=modulo.Route,
                State = modulo.State
            });

            return moduloDtos;
        }
        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }
        public async Task<ModuloDto> GetById(int id)
        {
            Modulo modulo = await this.data.GetById(id);
            if (modulo == null)
            {
                throw new Exception("Registro no encontrado"); 
            }
            ModuloDto moduloDto = new ModuloDto
            {
                Id = modulo.Id,
                Name = modulo.Name,
                Description = modulo.Description,  
                State = modulo.State

            };
            return moduloDto; 
        }

        public Modulo mapearDatos(Modulo modulo, ModuloDto entity)
        {
            modulo.Id = entity.Id; 
            modulo.Name = entity.Name;
            modulo.Description = entity.Description;
            modulo.Route = entity.Route; 
            modulo.State= entity.State;
            return modulo;
        }

        public async Task<Modulo>Save(ModuloDto entity)
        {
            Modulo modulo = new Modulo
            {
                CreatedAt = DateTime.Now.AddHours(-5)
            };
            modulo = this.mapearDatos(modulo, entity);
            return await this.data.Save(modulo);
        }

        public async Task Update(ModuloDto entity)
        {
            Modulo modulo = await this.data.GetById(entity.Id);
            if (modulo == null)
            {
                throw new Exception("Registro no encontrado"); 
            }
            modulo = this.mapearDatos(modulo, entity); 
            await this.data.Update(modulo);
        }


    }
}
