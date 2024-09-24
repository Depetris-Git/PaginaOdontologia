using Odontología.DB.Data;

namespace Odontología.Server.Repositorio
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> ChangeState(int id);
        Task<bool> Drop(int id);
        Task<bool> Exist(int id);
        Task<bool> HalfUpdate(int id, E entidad);
        Task<int> Insert(E entidad);
        Task<List<E>> Select();
        Task<List<E>> SelectActive();
        Task<E>? SelectById(int id);
        Task<E>? SelectByIdWithTracking(int id);
        Task<bool> Update(int id, E entidad);
    }
}