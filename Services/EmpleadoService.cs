using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiExamen.Models;

namespace ApiExamen.Services
{
    public class EmpleadoService
    {
        private readonly string _connectionString;
        
        public EmpleadoService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Empleado>> Consultar()
        {
            using var con = new SqlConnection(_connectionString);
            return await con.QueryAsync<Empleado>("spConsultarEmpleados", commandType: CommandType.StoredProcedure);
        }

        public async Task Crear(Empleado e)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spInsertarEmpleado", e, commandType: CommandType.StoredProcedure);
        }

        public async Task Actualizar(Empleado e)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spActualizarEmpleado", e, commandType: CommandType.StoredProcedure);
        }

        public async Task Eliminar(int codigoEmpleado)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spEliminarEmpleado", new { codigoEmpleado }, commandType: CommandType.StoredProcedure);
        }
    }
}
