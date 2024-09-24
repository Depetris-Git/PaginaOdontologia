using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public class PagoRepositorio : Repositorio<Pago>, IPagoRepositorio
    {
        private readonly Context context;
        public PagoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
