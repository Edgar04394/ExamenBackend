using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiExamen.Models;

namespace ApiExamen.Services
{
    public class PreguntaService
    {
        private readonly string _connectionString;
        
        public PreguntaService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Pregunta>> ConsultarPorExamen(int idExamen)
        {
            using var con = new SqlConnection(_connectionString);
            return await con.QueryAsync<Pregunta>("spConsultarPreguntasPorExamen", new { idExamen }, commandType: CommandType.StoredProcedure);
        }

        public async Task Crear(Pregunta p)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spInsertarPregunta", p, commandType: CommandType.StoredProcedure);
        }

        public async Task Actualizar(Pregunta p)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spActualizarPregunta", p, commandType: CommandType.StoredProcedure);
        }

        public async Task Eliminar(int idPregunta)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spEliminarPregunta", new { idPregunta }, commandType: CommandType.StoredProcedure);
        }
    }
}
