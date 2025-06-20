using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiExamen.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class AsignacionesController : ControllerBase
    {
        private readonly AsignacionService _asignacionService;

        public AsignacionesController(AsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        [HttpPost("AsignarExamenAEmpleado")]
        public async Task<IActionResult> AsignarExamen([FromBody] Asignacion asignacion)
        {
            await _asignacionService.Asignar(asignacion);
            return Ok("Examen asignado");
        }

        [HttpPost("VisualizarAsignacionesPorEmpleado")]
        public async Task<IActionResult> ListarAsignacionesPorEmpleado([FromBody] Empleado empleado)
        {
            var asignaciones = await _asignacionService.ConsultarPorEmpleado(empleado.codigoEmpleado);
            return Ok(asignaciones);
        }
    }
}
