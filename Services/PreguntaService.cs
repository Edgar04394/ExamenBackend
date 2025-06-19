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

            var parametros = new
            {
                textoPregunta = p.textoPregunta,       // asegúrate que coincida con el nombre del parámetro del SP
                idExamen = p.idExamen,
                idClasificacion = p.idClasificacion
            };

            await con.ExecuteAsync("spInsertarPregunta", parametros, commandType: CommandType.StoredProcedure);
        }


        public async Task Actualizar(Pregunta p)
        {
            using var con = new SqlConnection(_connectionString);

            var parametros = new
            {
                idPregunta = p.idPregunta,
                textoPregunta = p.textoPregunta,
                idClasificacion = p.idClasificacion
            };

            await con.ExecuteAsync("spActualizarPregunta", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task Eliminar(int idPregunta)
        {
            using var con = new SqlConnection(_connectionString);
            await con.ExecuteAsync("spEliminarPregunta", new { idPregunta }, commandType: CommandType.StoredProcedure);
        }
    }
}
