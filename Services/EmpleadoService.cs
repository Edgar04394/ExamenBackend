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

            // Obtener el siguiente códigoEmpleado iniciando desde 1
            var nuevoCodigo = await con.ExecuteScalarAsync<int>(
                "SELECT ISNULL(MAX(codigoEmpleado), 0) + 1 FROM Empleados");

            e.codigoEmpleado = nuevoCodigo;

            // Ejecutar el procedimiento almacenado
            await con.ExecuteAsync("spInsertarEmpleado", new
            {
                e.codigoEmpleado,
                e.nombre,
                e.apellidoPaterno,
                e.apellidoMaterno,
                e.fechaNacimiento,
                e.fechaInicioContrato,
                e.idPuesto
            }, commandType: CommandType.StoredProcedure);
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
