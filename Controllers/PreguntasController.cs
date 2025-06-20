using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiExamen.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntasController : ControllerBase
    {
        private readonly PreguntaService _preguntaService;

        public PreguntasController(PreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }

        [HttpPost("visualizarPreguntaPorExamen")]
        public async Task<IActionResult> VisualizarPreguntasPorExamen([FromBody] Examen examen)
        {
            var preguntas = await _preguntaService.ConsultarPorExamen(examen.idExamen);
            return Ok(preguntas);
        }

        [HttpPost("CrearPregunta")]
        public async Task<IActionResult> CrearPregunta([FromBody] Pregunta pregunta)
        {
            await _preguntaService.Crear(pregunta);
            return Ok("Pregunta creada");
        }

        [HttpPost("ActualizarPregunta/{id}")]
        public async Task<IActionResult> ActualizarPregunta(int id, [FromBody] Pregunta pregunta)
        {
            pregunta.idPregunta = id;
            await _preguntaService.Actualizar(pregunta);
            return Ok("Pregunta actualizada");
        }

        [HttpPost("EliminarPregunta/{id}")]
        public async Task<IActionResult> EliminarPregunta(int id)
        {
            await _preguntaService.Eliminar(id);
            return Ok("Pregunta eliminada");
        }
    }
}
