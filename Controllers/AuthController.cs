using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using ApiExamen.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _authService.Login(request);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            var token = _authService.GenerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.idUsuario,
                    usuario.usuario,
                    usuario.rol
                }
            });
        }
    }
}
