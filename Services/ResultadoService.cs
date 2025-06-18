using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiExamen.Models;

namespace ApiExamen.Services
{
    public class ResultadoService
    {
        private readonly string _connectionString;
        
        public ResultadoService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task GuardarRespuesta(RespuestaEmpleado r)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spInsertarRespuestaEmpleado", r, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ReportePromedioCompetencia>> ObtenerReporte(int idAsignacion)
        {
            using var con = new SqlConnection(_connectionString);
            return await con.QueryAsync<ReportePromedioCompetencia>("spReportePromedioPorCompetencia", new { idAsignacion }, commandType: CommandType.StoredProcedure);
        }
    }
}
