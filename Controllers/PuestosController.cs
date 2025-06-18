using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuestosController : ControllerBase
    {
        private readonly PuestoService _puestoService;

        public PuestosController(PuestoService puestoService)
        {
            _puestoService = puestoService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearPuesto([FromBody] Puesto puesto)
        {
            await _puestoService.Crear(puesto);
            return Ok("Puesto creado");
        }

        [HttpPost("listar")]
        public async Task<IActionResult> ListarPuestos()
        {
            var puestos = await _puestoService.Consultar();
            return Ok(puestos);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarPuesto([FromBody] Puesto puesto)
        {
            await _puestoService.Actualizar(puesto);
            return Ok("Puesto actualizado");
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> EliminarPuesto([FromBody] Puesto puesto)
        {
            await _puestoService.Eliminar(puesto.idPuesto);
            return Ok("Puesto eliminado");
        }
    }
}
