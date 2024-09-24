using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odontología.DB.Data.Entity;
using Odontología.Server.Repositorio;
using Odontología.Shared.DTO;

namespace Odontología.Server.Controllers
{
    [ApiController]
    [Route("api/Pagos")]
    public class PagosControllers : ControllerBase
    {

        private readonly IPagoRepositorio repositorio;
        private readonly IPresupuestoRepositorio presRepositorio;
        private readonly IMapper mapper;
        public PagosControllers(IPagoRepositorio repositorio,
                                IPresupuestoRepositorio presRepositorio,
                                            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.presRepositorio = presRepositorio;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] PagoDTO entidadDTO)
        {
            Presupuesto presupuesto = await presRepositorio.SelectByIdWithTracking(entidadDTO.PresupuestoId);

            if (presupuesto == null)
            {
                return NotFound();
            }

            var entidad = mapper.Map<Pago>(entidadDTO);
            if(presupuesto.Pagos == null) presupuesto.Pagos = new List<Pago>();
            try
            {
                await repositorio.Insert(entidad);
                presupuesto.Pagos.Add(entidad);
                await presRepositorio.SimpleUpdate(presupuesto);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }

            try
            {
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
    }
}
