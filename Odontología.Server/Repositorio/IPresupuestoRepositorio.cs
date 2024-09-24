using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public interface IPresupuestoRepositorio : IRepositorio<Presupuesto>
    {
        Task<List<Presupuesto>> FullGetAll();
        Task<Presupuesto> FullGetById(int id);
        Task<List<Presupuesto>> GetWithPagos();
        Task<int> SimpleInsert(Presupuesto entidad);
        Task<int> SimpleUpdate(Presupuesto entidad);
    }
}
