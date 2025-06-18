using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsignacionesController : ControllerBase
    {
        private readonly AsignacionService _asignacionService;

        public AsignacionesController(AsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarExamen([FromBody] Asignacion asignacion)
        {
            await _asignacionService.Asignar(asignacion);
            return Ok("Examen asignado");
        }

        [HttpPost("listarPorEmpleado")]
        public async Task<IActionResult> ListarAsignacionesPorEmpleado([FromBody] Empleado empleado)
        {
            var asignaciones = await _asignacionService.ConsultarPorEmpleado(empleado.codigoEmpleado);
            return Ok(asignaciones);
        }
    }
}
