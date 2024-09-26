using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IModulocontroller
    {

        // este metodo devolvera una accion que contienen-lista-objetos de ModuloDto
        public Task<ActionResult<IEnumerable<ModuloDto>>> GetAll();

        //resibe un objeto ModuloDto como parametro, este viene de una solicitud HTTP-FromBody
        //retorna Task-contiene-actionResult que devuelve-nuevo-objeto de ModuloDto que se guardo
        public Task<ActionResult<ModuloDto>> Save([FromBody] ModuloDto moduloDto);

        //resibe-obj-ModuloDto-viene-solucion HTTP retorna un task-IActionResult
        //que es interfaz para representar varios tipos de respuestas HTTP.
        public Task<IActionResult> Update ([FromBody] ModuloDto moduloDto);

        //resibe int-IActionResult-indica el resultado de la acción de eliminar.
        public Task<IActionResult> Delete(int id);

    }
}
