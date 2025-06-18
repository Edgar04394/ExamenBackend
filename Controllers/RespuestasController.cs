using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestasController : ControllerBase
    {
        private readonly RespuestaService _respuestaService;

        public RespuestasController(RespuestaService respuestaService)
        {
            _respuestaService = respuestaService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearRespuesta([FromBody] Respuesta respuesta)
        {
            await _respuestaService.Crear(respuesta);
            return Ok("Respuesta creada");
        }

        [HttpPost("listarPorPregunta")]
        public async Task<IActionResult> ListarRespuestasPorPregunta([FromBody] Pregunta pregunta)
        {
            var respuestas = await _respuestaService.ConsultarPorPregunta(pregunta.idPregunta);
            return Ok(respuestas);
        }

        [HttpPost("eliminarPorPregunta")]
        public async Task<IActionResult> EliminarRespuestasPorPregunta([FromBody] Pregunta pregunta)
        {
            await _respuestaService.EliminarPorPregunta(pregunta.idPregunta);
            return Ok("Respuestas eliminadas");
        }
    }
}
