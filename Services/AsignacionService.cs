using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiExamen.Models;

namespace ApiExamen.Services
{
    public class AsignacionService
    {
        private readonly string _connectionString;
        
        public AsignacionService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task Asignar(Asignacion a)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spAsignarExamen", a, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Asignacion>> ConsultarPorEmpleado(int codigoEmpleado)
        {
            using var con = new SqlConnection(_connectionString);
            return await con.QueryAsync<Asignacion>("spConsultarAsignacionesEmpleado", new { codigoEmpleado }, commandType: CommandType.StoredProcedure);
        }
    }
}
