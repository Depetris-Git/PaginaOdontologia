using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odontología.DB.Data.Entity;
using Odontología.Server.Repositorio;
using Odontología.Shared.DTO;

namespace Odontología.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuestos")]
    public class PresupuestosControllers : ControllerBase
    {

        private readonly IPresupuestoRepositorio repositorio;
        private readonly IPagoRepositorio pagRepositorio;
        private readonly IMapper mapper;
        public PresupuestosControllers(IPresupuestoRepositorio repositorio,
                                        IPagoRepositorio pagRepositorio,
                                            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.pagRepositorio = pagRepositorio;
            this.mapper = mapper;
        }
        [HttpGet("Full")]
        public async Task<ActionResult<List<Presupuesto>>> GetAll()
        {
            return await repositorio.FullGetAll();
        }
        [HttpGet("ConPagos")]
        public async Task<ActionResult<List<Presupuesto>>> GetWithPagos()
        {
            return await repositorio.GetWithPagos();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            return await repositorio.FullGetById(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Insert(PresupuestoDTO entidadDTO)
        {
            var entidad = mapper.Map<Presupuesto>(entidadDTO);
            entidad.Pagos = new List<Pago>();
            await repositorio.SimpleInsert(entidad);
            return Ok("Creado Exitosamente");
        }
        [HttpPut]
        public async Task<ActionResult<int>> Update(PresupuestoDTO entidadDTO)
        {
            var entidad = mapper.Map<Presupuesto>(entidadDTO);
            await repositorio.SimpleUpdate(entidad);
            return Ok($"El presupuesto de Id {entidad.Id} ha sido actualizado");
        }
        [HttpPut("CambioEstado/{id:int}")] //api/TipoTratamientos/CambioEstado/*Id del TipoTratamiento*
        public async Task<ActionResult> PutChangeState(int id)
        {
            string result;
            var flag = await repositorio.ChangeState(id);
            Presupuesto? entidadReg = await repositorio.SelectById(id);

            if (flag == false)
            {
                return NotFound("No existe Tratamiento buscado");
            }
            if (entidadReg.Activo == true)
            {
                result = "activado";
            }
            else
            {
                result = "desactivado";
            }
            try
            {
                return Ok($"El tratamiento número '{entidadReg.Id}' ahora está {result}.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await repositorio.Exist(id);
            if (!existe)
            {
                return BadRequest($"El tipo de tratamiento a borrar (Id: {id}) no existe");
            }
            if (await repositorio.Drop(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
