using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadosController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {
            await _empleadoService.Crear(empleado); // ← nombre correcto
            return Ok("Empleado creado");
        }

        [HttpPost("listar")]
        public async Task<IActionResult> ListarEmpleados()
        {
            var empleados = await _empleadoService.Consultar(); // ← nombre correcto
            return Ok(empleados);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarEmpleado([FromBody] Empleado empleado)
        {
            await _empleadoService.Actualizar(empleado); // ← nombre correcto
            return Ok("Empleado actualizado");
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> EliminarEmpleado([FromBody] Empleado empleado)
        {
            await _empleadoService.Eliminar(empleado.codigoEmpleado); // ← nombre correcto
            return Ok("Empleado eliminado");
        }

    }
}
