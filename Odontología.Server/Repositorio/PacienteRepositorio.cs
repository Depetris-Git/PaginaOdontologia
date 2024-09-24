using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Repositorio
{
    public class PacienteRepositorio : Repositorio<Paciente>, IPacienteRepositorio
    {
        private readonly Context context;
        public PacienteRepositorio(Context context) : base (context)
        {
            this.context = context;
        }
        public async Task<List<Paciente>> GetWithTratamientos()
        {
            return await context.Pacientes
                .Include(p => p.Historial_TratamientosOd)
                .ToListAsync();
        }
        public async Task<List<Paciente>> GetWithPresupuestos()
        {
            return await context.Pacientes
                .Include(p => p.Historial_Presupuestos)
                .ToListAsync();
        }
        public async Task<List<Paciente>> FullGetAll()
        {
            return await context.Pacientes
                .Include(p => p.Historial_Presupuestos)
                .Include(p => p.Historial_TratamientosOd)
                .ToListAsync();
        }
        public async Task<Paciente> FullGetById(int id)
        {
            return await context.Pacientes
                .Include(p => p.Historial_Presupuestos)
                .Include(p => p.Historial_TratamientosOd)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Paciente>> GetByName(string name)
        {
            var entidadReg = await context.Pacientes
                         .Where(x => x.NombreCompleto == name).ToListAsync();
            return entidadReg;
        }
    }
}
