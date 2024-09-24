using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public class TratamientoOdRepositorio : Repositorio<TratamientoOd>, ITratamientoOdRepositorio
    {
        private readonly Context context;
        public TratamientoOdRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<TratamientoOd> FullGetById(int id)
        {
            try
            {
                var pepe = await context.TratamientosOd.FirstOrDefaultAsync(p => p.Id == id);
                if (pepe.PresupuestoId != null)
                {
                    return await context.TratamientosOd
                    .Include(p => p.TipoTratamiento)
                    .Include(p => p.Paciente)
                    .Include(p => p.Presupuesto)
                    .FirstOrDefaultAsync(p => p.Id == id);
                }
                else
                {
                    return await context.TratamientosOd
                    .Include(p => p.TipoTratamiento)
                    .Include(p => p.Paciente)
                    .FirstOrDefaultAsync(p => p.Id == id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<List<TratamientoOd>> FullGetAll()
        {
            return await context.TratamientosOd
                .Include(p => p.TipoTratamiento)
                .Include(p=> p.Paciente)
                .ToListAsync();
        }

        public async Task<List<TratamientoOd>> FullGetActive()
        {
            return await context.TratamientosOd.Where(e => e.Activo == true)
                .Include(p => p.TipoTratamiento)
                .ToListAsync();
        }

        public async Task<int> SimpleInsert(TratamientoOd entidad)
        {
            try
            {
                entidad.CostoTotal = entidad.CostoAcordado + entidad.CostoProtesista;
                await context.TratamientosOd.AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public async Task<int> SimpleUpdate(TratamientoOd entidad)
        {
            try
            {
                entidad.CostoTotal = entidad.CostoAcordado + entidad.CostoProtesista;
                context.TratamientosOd.Update(entidad);
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
