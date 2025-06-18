using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntasController : ControllerBase
    {
        private readonly PreguntaService _preguntaService;

        public PreguntasController(PreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearPregunta([FromBody] Pregunta pregunta)
        {
            await _preguntaService.Crear(pregunta);
            return Ok("Pregunta creada");
        }

        [HttpPost("listarPorExamen")]
        public async Task<IActionResult> ListarPreguntasPorExamen([FromBody] Examen examen)
        {
            var preguntas = await _preguntaService.ConsultarPorExamen(examen.idExamen);
            return Ok(preguntas);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarPregunta([FromBody] Pregunta pregunta)
        {
            await _preguntaService.Actualizar(pregunta);
            return Ok("Pregunta actualizada");
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> EliminarPregunta([FromBody] Pregunta pregunta)
        {
            await _preguntaService.Eliminar(pregunta.idPregunta);
            return Ok("Pregunta eliminada");
        }
    }
}
