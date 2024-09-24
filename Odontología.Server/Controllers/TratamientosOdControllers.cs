using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odontología.DB.Data.Entity;
using Odontología.Server.Repositorio;
using Odontología.Shared.DTO;

namespace Odontología.Server.Controllers
{
    [ApiController]
    [Route("api/TratamientosOd")]
    public class TratamientosOdControllers : ControllerBase
    {

        private readonly ITratamientoOdRepositorio repositorio;
        private readonly IPacienteRepositorio pacienteRepositorio;
        private readonly IPresupuestoRepositorio presRepositorio;
        private readonly IMapper mapper;
        public TratamientosOdControllers(ITratamientoOdRepositorio repositorio,
                                         IPacienteRepositorio pacienteRepositorio,
                                         IPresupuestoRepositorio presRepositorio,
                                            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.pacienteRepositorio = pacienteRepositorio;
            this.presRepositorio = presRepositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TratamientoOd>>> Get()
        {
            return await repositorio.FullGetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TratamientoOd>> Get(int id)
        {
            var entidadReg = await repositorio.FullGetById(id);

            if (entidadReg == null)
            {
                return NotFound();
            }
            return entidadReg;
        }

        [HttpGet("Existe/{id:int}")]
        public async Task<ActionResult<bool>> Exist(int id)
        {
            var existe = await repositorio.Exist(id);

            return existe;
        }

        [HttpGet("SoloActivos")]
        public async Task<ActionResult<List<TratamientoOd>>> GetOnlyActive()
        {
            return await repositorio.FullGetActive();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] TratamientoOdDTO entidadDTO)
        {
            Paciente paciente = await pacienteRepositorio.SelectById(entidadDTO.PacienteId);

            if (paciente == null)
            {
                return NotFound();
            }

            var entidad = mapper.Map<TratamientoOd>(entidadDTO);
            entidad.CostoTotal = entidad.CostoAcordado + entidad.CostoProtesista;

            try
            {
                await repositorio.Insert(entidad);
                paciente.Historial_TratamientosOd.Add(entidad);
                await pacienteRepositorio.Update(entidadDTO.PacienteId, paciente);
                if (entidad.PresupuestoId != null)
                {
                    var pres = await presRepositorio.SelectById(entidad.PresupuestoId.Value);
                    await presRepositorio.SimpleUpdate(pres);
                }
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

        [HttpPut("CambioEstado/{id:int}")] //api/TipoTratamientos/CambioEstado/*Id del TipoTratamiento*
        public async Task<ActionResult> PutChangeState(int id)
        {
            string result;
            var flag = await repositorio.ChangeState(id);
            TratamientoOd? entidadReg = await repositorio.SelectById(id);

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

        [HttpPut("{id:int}")] //api/TipoTratamientos/*Id del TipoTratamiento*
        public async Task<ActionResult> Put(int id, [FromBody] TratamientoOdDTO entidadDTO)
        {
            TratamientoOd? entidad = await repositorio.SelectById(id);
            if (entidad == null)
            {
                return NotFound($"No se encontró el tratamiento correspondiente a la ID: {id}.");
            }
            entidad = mapper.Map<TratamientoOd>(entidadDTO);
            entidad.Id = id;

            try
            {
                if (await repositorio.SimpleUpdate(entidad) != null)
                {
                    if (entidad.PresupuestoId != null)
                    {
                        var pres = await presRepositorio.SelectByIdWithTracking(entidad.PresupuestoId.Value);
                        await presRepositorio.SimpleUpdate(pres);
                    }
                    return Ok($"El tratamiento de ID: {entidad.Id} ha sido modificado correctamente.");
                }
                else
                {
                    return BadRequest("Algo salió mal.");
                }

            }
            catch (Exception? e)
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
                return BadRequest($"El tratamiento a borrar (Id: {id}) no existe");
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
