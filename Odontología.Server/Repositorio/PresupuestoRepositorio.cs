using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public class PresupuestoRepositorio : Repositorio<Presupuesto>, IPresupuestoRepositorio
    {
        private readonly Context context;
        public PresupuestoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<Presupuesto>> GetWithPagos()
        {
            return await context.Presupuestos
                .Include(p => p.Pagos)
                .ToListAsync();
        }
        public async Task<List<Presupuesto>> FullGetAll()
        {
            return await context.Presupuestos
                .Include(p => p.UltimoPago)
                .ToListAsync();
        }
        public async Task<Presupuesto> FullGetById(int id)
        {
            return await context.Presupuestos
                .Include(p => p.UltimoPago)
                .Include(p => p.Pagos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> SimpleInsert(Presupuesto entidad)
        {
            entidad.TratamientosOd = new List<TratamientoOd>();
            entidad.Pagos = new List<Pago>();
            entidad.CostoTotal = entidad.CostoIncial;
            entidad.CostoAbonado = 0;
            entidad.CostoPorPagar = entidad.CostoTotal - entidad.CostoAbonado;
            if (entidad.CostoPorPagar > 0)
            {
                entidad.Pagado = true;
            }
            else
            {
                entidad.Pagado = false;
            }
            try
            {

                await context.Presupuestos.AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public async Task<int> SimpleUpdate(Presupuesto entidad)
        {
            if (entidad.Pagos == null) entidad.Pagos = new List<Pago>();
            if (entidad.TratamientosOd == null) entidad.TratamientosOd = new List<TratamientoOd>();
            if (entidad.TratamientosOd.Count() > 0)
            {
                var primerTrat = entidad.TratamientosOd.OrderBy(x => x.FechaCreacion).FirstOrDefault();
                entidad.CostoIncial = primerTrat.CostoTotal;
                entidad.CostoTotal = entidad.TratamientosOd.Sum(x => x.CostoTotal);
            }
            entidad.UltimoPago = entidad.Pagos.OrderByDescending(x=> x.FechaPago).FirstOrDefault();
            entidad.CostoAbonado = entidad.Pagos?.Sum(pago => pago.Monto) ?? 0;
            try
            {
                entidad.CostoPorPagar = entidad.CostoTotal - entidad.CostoAbonado;
                if (entidad.CostoPorPagar > 0)
                {
                    entidad.Pagado = false;
                }
                else
                {
                    entidad.Pagado = true;
                }
                context.Presupuestos.Update(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
    }
}
