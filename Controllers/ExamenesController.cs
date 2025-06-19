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

        [HttpPost("VisualizarExamen")]
        public async Task<IActionResult> VisualizarExamenes()
        {
            var examenes = await _examenService.Consultar();
            return Ok(examenes);
        }

        [HttpPost("CrearExamen")]
        public async Task<IActionResult> CrearExamen([FromBody] Examen examen)
        {
            await _examenService.Crear(examen);
            return Ok("Examen creado");
        }

        [HttpPost("ActualizarExamen/{id}")]
        public async Task<IActionResult> ActualizarExamen(int id, [FromBody] Examen examen)
        {
            examen.idExamen = id;
            await _examenService.Actualizar(examen);
            return Ok("Examen actualizado");
        }

        [HttpPost("EliminarExamen/{id}")]
        public async Task<IActionResult> EliminarExamen(int id)
        {
            await _examenService.Eliminar(id);
            return Ok("Examen eliminado");
        }
    }
}
