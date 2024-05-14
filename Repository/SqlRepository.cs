using CitasMedicas.Models;
using CitasMedicas.Repository.IRepository;
using Microsoft.Data.Sqlite;

namespace CitasMedicas.Repository
{
    public class SqlRepository : ISqlRepository
    {


        private readonly string _connectionString;

        public SqlRepository(string? connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<CitasMedico> CrearCitaMedico(CitasMedico c)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCitaMedicaById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CitasMedico>> GetAllCitas()
        {

            var citasMedico = new List<CitasMedico>();
            using (var connection = new SqliteConnection(_connectionString))
            {

                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {

                    var sql = @"SELECT * FROM CitasMedico";

                    command.CommandText = sql;


                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            var cita = new CitasMedico
                            (
                            citaId: reader.GetInt32(reader.GetOrdinal("CitaId")),
                            pacienteId: reader.GetInt32(reader.GetOrdinal("PacienteId")),
                            doctorId: reader.GetInt32(reader.GetOrdinal("DoctorId")),
                            fechaHoraInicio: reader.GetDateTime(reader.GetOrdinal("FechaHoraInicio")),
                            fechaHoraFin: reader.GetDateTime(reader.GetOrdinal("FechaHoraFin")),
                            estado: reader.GetString(reader.GetOrdinal("Estado")),
                            comentarios: reader.GetString(reader.GetOrdinal("Comentarios"))
                            );
                            citasMedico.Add(cita); // Añadir la cita a la lista de resultados

                        }
                    }


                }
            }

            return citasMedico;
        }

        public Task<CitasMedico> GetCitaMedicaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CitasMedico?> GetCitaMedicaByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<CitasMedico> UpdateCitasMedico(CitasMedico c)
        {
            throw new NotImplementedException();
        }
    }
}
