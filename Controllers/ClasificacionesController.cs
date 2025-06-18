using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasificacionesController : ControllerBase
    {
        private readonly ClasificacionService _clasificacionService;

        public ClasificacionesController(ClasificacionService clasificacionService)
        {
            _clasificacionService = clasificacionService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearClasificacion([FromBody] Clasificacion clasificacion)
        {
            await _clasificacionService.Crear(clasificacion);
            return Ok("Clasificación creada");
        }

        [HttpPost("listar")]
        public async Task<IActionResult> ListarClasificaciones()
        {
            var clasificaciones = await _clasificacionService.Consultar();
            return Ok(clasificaciones);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarClasificacion([FromBody] Clasificacion clasificacion)
        {
            await _clasificacionService.Actualizar(clasificacion);
            return Ok("Clasificación actualizada");
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> EliminarClasificacion([FromBody] Clasificacion clasificacion)
        {
            await _clasificacionService.Eliminar(clasificacion.idClasificacion);
            return Ok("Clasificación eliminada");
        }
    }
}
