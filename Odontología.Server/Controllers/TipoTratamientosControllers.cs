using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;

namespace Odontología.Server.Controllers
{
    [ApiController]
    [Route("TipoTratamientos")]
    public class TipoTratamientosControllers : ControllerBase
    {
        private readonly Context context;
        public TipoTratamientosControllers(Context context)
        {
            this.context = context;   
        }
        [HttpGet]
        public async Task<ActionResult<List<TipoTratamiento>>> Get()
        {
            return await context.TipoTratamientos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(TipoTratamiento entidad)
        {
            try
            {
                context.TipoTratamientos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
