using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public interface ITratamientoOdRepositorio : IRepositorio<TratamientoOd>
    {
        Task<List<TratamientoOd>> FullGetActive();
        Task<List<TratamientoOd>> FullGetAll();
        Task<TratamientoOd> FullGetById(int id);
        Task<int> SimpleInsert(TratamientoOd entidad);
        Task<int> SimpleUpdate(TratamientoOd entidad);
    }
}
