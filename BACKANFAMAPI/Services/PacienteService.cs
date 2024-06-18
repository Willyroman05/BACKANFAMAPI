using Microsoft.Data.SqlClient;

namespace BACKANFAMAPI.Services
{
    public class PacienteService
    {
        private readonly string _connectionString;

        public PacienteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ANFAM_DataBase");
        }

        public async Task<List<PacienteDto>> GetPacientesAsync()
        {
            var pacientes = new List<PacienteDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetPaciente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            pacientes.Add(new PacienteDto
                            {
                                PrimerNombre = reader["PRIMER_NOMBRE"].ToString(),
                                SegundoApellido = reader["SEGUNDO_APELLIDO"].ToString(),
                                Embarazo = reader["EMBARAZO"].ToString(),
                                CodAntPer = int.Parse(reader["COD_ANTPER"].ToString())
                            });
                        }
                    }
                }
            }

            return pacientes;
        }
    }
}

