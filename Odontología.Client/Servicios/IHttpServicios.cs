
namespace Odontología.Client.Servicios
{
    public interface IHttpServicios
    {
        Task<HttpRespuesta<T>> Get<T>(string url);
    }
}