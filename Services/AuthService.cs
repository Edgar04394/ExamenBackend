using Dapper;
using Microsoft.Data.SqlClient;
using ApiExamen.Models;
// Elimina el using que genera conflicto:
// using Microsoft.AspNetCore.Identity.Data;

namespace ApiExamen.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Usuario?> Login(LoginRequest request)
        {
            using var con = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Usuarios WHERE usuario = @usuario AND contrasena = @contrasena";

            // Usa las propiedades con mayúsculas, como están definidas en el modelo
            return await con.QueryFirstOrDefaultAsync<Usuario>(sql, new
            {
                usuario = request.Usuario,
                contrasena = request.Contrasena
            });
        }
    }
}
