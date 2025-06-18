using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultadosController : ControllerBase
    {
        private readonly ResultadoService _resultadoService;

        public ResultadosController(ResultadoService resultadoService)
        {
            _resultadoService = resultadoService;
        }

        [HttpPost("guardarRespuesta")]
        public async Task<IActionResult> GuardarRespuestaEmpleado([FromBody] RespuestaEmpleado respuestaEmpleado)
        {
            await _resultadoService.GuardarRespuesta(respuestaEmpleado);
            return Ok("Respuesta guardada");
        }

        [HttpPost("reportePorCompetencia")]
        public async Task<IActionResult> ReportePromedioPorCompetencia([FromBody] Asignacion asignacion)
        {
            var reporte = await _resultadoService.ObtenerReporte(asignacion.idAsignacion);
            return Ok(reporte);
        }
    }
}
