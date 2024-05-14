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
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO CitasMedico (PacienteId, DoctorId, FechaHoraInicio, FechaHoraFin, Estado, Comentarios)
            VALUES (@pacienteId, @doctorId, @fechaHoraInicio, @fechaHoraFin, @estado, @comentarios)";

                command.Parameters.AddWithValue("@pacienteId", c.PacienteId);
                command.Parameters.AddWithValue("@doctorId", c.DoctorId);
                command.Parameters.AddWithValue("@fechaHoraInicio", c.FechaHoraInicio);
                command.Parameters.AddWithValue("@fechaHoraFin", c.FechaHoraFin);
                command.Parameters.AddWithValue("@estado", c.Estado);
                command.Parameters.AddWithValue("@comentarios", c.Comentarios ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
                //c.CitaId = (int)connection;
                return c;
            }
        }


        public async Task<bool> DeleteCitaMedicaById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            DELETE FROM CitasMedico WHERE CitaId = @citaId";
                command.Parameters.AddWithValue("@citaId", id);

                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
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

        public async Task<CitasMedico> GetCitaMedicaById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    var sql = @"SELECT CitaId, PacienteId, DoctorId, FechaHoraInicio, FechaHoraFin, Estado, Comentarios 
                        FROM CitasMedico 
                        WHERE CitaId = @CitaId";

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@CitaId", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var cita = new CitasMedico
                            (
                                citaId: reader.GetInt32(reader.GetOrdinal("CitaId")),
                                pacienteId: reader.GetInt32(reader.GetOrdinal("PacienteId")),
                                doctorId: reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                fechaHoraInicio: reader.GetDateTime(reader.GetOrdinal("FechaHoraInicio")),
                                fechaHoraFin: reader.GetDateTime(reader.GetOrdinal("FechaHoraFin")),
                                estado: reader.GetString(reader.GetOrdinal("Estado")),
                                comentarios: reader.IsDBNull(reader.GetOrdinal("Comentarios")) ? null : reader.GetString(reader.GetOrdinal("Comentarios"))
                            );
                            return cita;
                        }
                    }
                }
            }
            return null; // Retorna null si no se encuentra la cita
        }


        public async Task<CitasMedico?> GetCitaMedicaByName(string name)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT * FROM CitasMedico
            WHERE PacienteNombre = @name";
                command.Parameters.AddWithValue("@name", name);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CitasMedico(
                            citaId: reader.GetInt32(reader.GetOrdinal("CitaId")),
                            pacienteId: reader.GetInt32(reader.GetOrdinal("PacienteId")),
                            doctorId: reader.GetInt32(reader.GetOrdinal("DoctorId")),
                            fechaHoraInicio: reader.GetDateTime(reader.GetOrdinal("FechaHoraInicio")),
                            fechaHoraFin: reader.GetDateTime(reader.GetOrdinal("FechaHoraFin")),
                            estado: reader.GetString(reader.GetOrdinal("Estado")),
                            comentarios: reader.IsDBNull(reader.GetOrdinal("Comentarios")) ? null : reader.GetString(reader.GetOrdinal("Comentarios"))
                        );
                    }
                }
            }
            return null;
        }


        public async Task<CitasMedico> UpdateCitasMedico(CitasMedico c)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {

                await connection.OpenAsync();


                using (var command = connection.CreateCommand())
                {

                    command.CommandText = @"
                UPDATE CitasMedico
                SET
                PacienteId = @pacienteId,
                DoctorId = @doctorId,
                FechaHoraInicio = @fechaHoraInicio,
                FechaHoraFin = @fechaHoraFin,
                Estado = @estado,
                Comentarios = @comentarios
                WHERE CitaId = @citaId";


                    command.Parameters.AddWithValue("@pacienteId", c.PacienteId);
                    command.Parameters.AddWithValue("@doctorId", c.DoctorId);
                    command.Parameters.AddWithValue("@fechaHoraInicio", c.FechaHoraInicio);
                    command.Parameters.AddWithValue("@fechaHoraFin", c.FechaHoraFin);
                    command.Parameters.AddWithValue("@estado", c.Estado);
                    command.Parameters.AddWithValue("@comentarios", c.Comentarios ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@citaId", c.CitaId);

                    await command.ExecuteNonQueryAsync();

                    return c;


                }
            }
        }
    }
}
