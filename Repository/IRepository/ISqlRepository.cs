using CitasMedicas.Models;

namespace CitasMedicas.Repository.IRepository
{
    public interface ISqlRepository
    {

        Task<IEnumerable<CitasMedico>> GetAllCitas();

        Task<CitasMedico> GetCitaMedicaById(int id);

        Task<CitasMedico?> GetCitaMedicaByName(string name);

        Task<CitasMedico> UpdateCitasMedico(CitasMedico c);

        Task<bool> DeleteCitaMedicaById(int id);

        Task<CitasMedico> CrearCitaMedico(CitasMedico c);
    }
}
