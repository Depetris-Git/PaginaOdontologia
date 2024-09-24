using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public class TipoTratamientoRepositorio : Repositorio<TipoTratamiento>, ITipoTratamientoRepositorio
    {
        private readonly Context context;
        public TipoTratamientoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
