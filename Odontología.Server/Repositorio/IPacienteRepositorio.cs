using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public interface IPacienteRepositorio : IRepositorio<Paciente>
    {
        Task<List<Paciente>> FullGetAll();
        Task<Paciente> FullGetById(int id);
        Task<List<Paciente>> GetByName(string name);
        Task<List<Paciente>> GetWithPresupuestos();
        Task<List<Paciente>> GetWithTratamientos();
    }
}
