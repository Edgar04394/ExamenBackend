using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiExamen.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadosController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpPost("visualizarEmpleado")]
        public async Task<IActionResult> VisualizarEmpleados()
        {
            var empleados = await _empleadoService.Consultar();
            return Ok(empleados);
        }

        [HttpPost("CrearEmpleado")]
        public async Task<IActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {
            await _empleadoService.Crear(empleado); // ← nombre correcto
            return Ok("Empleado creado");
        }

        [HttpPost("ActualizarEmpleado/{id}")]
        public async Task<IActionResult> ActualizarEmpleado(int id, [FromBody] Empleado empleado)
        {
            empleado.codigoEmpleado = id;
            await _empleadoService.Actualizar(empleado);
            return Ok("Empleado actualizado");
        }

        [HttpPost("EliminarEmpleado/{id}")]
        public async Task<IActionResult> EliminarEmpleado(int id)
        {
            await _empleadoService.Eliminar(id);
            return Ok("Empleado eliminado");
        }
    }
}
