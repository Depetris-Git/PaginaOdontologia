using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E>
                    where E : class, IEntityBase
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }
        public async Task<bool> Exist(int id)
        {
            var existe = await context.Set<E>().AnyAsync(x => x.Id == id);
            return existe;
        }

        public async Task<List<E>> SelectActive()
        {
            return await context.Set<E>()
                .Where(e => e.Activo == true)
                .ToListAsync();
        }
        public async Task<E>? SelectById(int id)
        {
            E? entidadReg = await context.Set<E>().AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);
            return entidadReg;
        }
        public async Task<E>? SelectByIdWithTracking(int id)
        {
            E? entidadReg = await context.Set<E>()
                                .FirstOrDefaultAsync(x => x.Id == id);
            return entidadReg;
        }
        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            var entidadReg = SelectById(id);

            if (entidadReg == null)
            {
                return false;
            }

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public async Task<bool> HalfUpdate(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            var entidadReg = SelectById(id);

            if (entidadReg == null)
            {
                return false;
            }

            try
            {
                context.Set<E>().Update(entidad);
                return true;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public async Task<bool> FastUpdate(E entidad)
        {
            try
            {
                context.Set<E>().Update(entidad);
                return true;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public async Task<bool> ChangeState(int id)
        {
            var entidadReg = await context.Set<E>()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (entidadReg == null)
            {
                return false;
            }
            else
            {
                if (entidadReg.Activo)
                {
                    entidadReg.Activo = false;
                }
                else
                {
                    entidadReg.Activo = true;
                }

                try
                {
                    context.Set<E>().Update(entidadReg);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        public async Task<bool> Drop(int id)
        {
            var existe = await Exist(id);
            if (!existe)
            {
                return false;
            }
            var entidadReg = await SelectById(id);

            if (entidadReg == null)
            {
                return false;
            }
            context.Set<E>().Remove(entidadReg);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
