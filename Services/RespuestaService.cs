using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiExamen.Models;

namespace ApiExamen.Services
{
    public class RespuestaService
    {
        private readonly string _connectionString;
        
        public RespuestaService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Respuesta>> ConsultarPorPregunta(int idPregunta)
        {
            using var con = new SqlConnection(_connectionString);
            return await con.QueryAsync<Respuesta>("spConsultarRespuestasPorPregunta", new { idPregunta }, commandType: CommandType.StoredProcedure);
        }

        public async Task Crear(Respuesta r)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spInsertarRespuesta", r, commandType: CommandType.StoredProcedure);
        }

        public async Task EliminarPorPregunta(int idPregunta)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spEliminarRespuestasPorPregunta", new { idPregunta }, commandType: CommandType.StoredProcedure);
        }
    }
}
