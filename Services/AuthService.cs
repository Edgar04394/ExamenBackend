using Dapper;
using Microsoft.Data.SqlClient;
using ApiExamen.Models;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiExamen.Services
{
    public class AuthService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
            _config = config;
        }

        public async Task<Usuario?> Login(LoginRequest request)
        {
            using var con = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Usuarios WHERE usuario = @usuario AND contrasena = @contrasena";
            return await con.QueryFirstOrDefaultAsync<Usuario>(sql, new
            {
                usuario = request.Usuario,
                contrasena = request.Contrasena
            });
        }

        public string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.usuario!),
            new Claim("id", usuario.idUsuario.ToString()),
            new Claim("rol", usuario.rol ?? "Empleado"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
