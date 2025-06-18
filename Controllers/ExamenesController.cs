using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamenesController : ControllerBase
    {
        private readonly ExamenService _examenService;

        public ExamenesController(ExamenService examenService)
        {
            _examenService = examenService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearExamen([FromBody] Examen examen)
        {
            await _examenService.Crear(examen);
            return Ok("Examen creado");
        }

        [HttpPost("listar")]
        public async Task<IActionResult> ListarExamenes()
        {
            var examenes = await _examenService.Consultar();
            return Ok(examenes);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarExamen([FromBody] Examen examen)
        {
            await _examenService.Actualizar(examen);
            return Ok("Examen actualizado");
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> EliminarExamen([FromBody] Examen examen)
        {
            await _examenService.Eliminar(examen.idExamen);
            return Ok("Examen eliminado");
        }
    }
}
