using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiExamen.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PuestosController : ControllerBase
    {
        private readonly PuestoService _puestoService;

        public PuestosController(PuestoService puestoService)
        {
            _puestoService = puestoService;
        }

        [HttpPost("visualizarPuesto")]
        public async Task<IActionResult> VisualizarPuestos()
        {
            var puestos = await _puestoService.Consultar();
            return Ok(puestos);
        }

        [HttpPost("CrearPuesto")]
        public async Task<IActionResult> CrearPuesto([FromBody] Puesto puesto)
        {
            await _puestoService.Crear(puesto);
            return Ok("Puesto creado");
        }

        [HttpPost("ActualizarPuesto/{id}")]
        public async Task<IActionResult> ActualizarPuesto(int id, [FromBody] Puesto puesto)
        {
            puesto.idPuesto = id;
            await _puestoService.Actualizar(puesto);
            return Ok("Puesto actualizado");
        }

        [HttpPost("EliminarPuesto/{id}")]
        public async Task<IActionResult> EliminarPuesto(int id)
        {
            await _puestoService.Eliminar(id);
            return Ok("Puesto eliminado");
        }
    }
}
