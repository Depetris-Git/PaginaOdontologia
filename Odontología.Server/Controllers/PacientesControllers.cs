using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;
using Odontología.Server.Repositorio;
using Odontología.Shared.DTO;

namespace Odontología.Server.Controllers
{
    [ApiController]
    [Route("api/Pacientes")]
    public class PacientesControllers : ControllerBase
    {
        private readonly IPacienteRepositorio repositorio;
        private readonly IMapper mapper;
        public PacientesControllers(IPacienteRepositorio repositorio,
                                    IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> Get()
        {
            return await repositorio.Select();
        }
        [HttpGet("ConTratamientos")]
        public async Task<ActionResult<List<Paciente>>> GetWithTratamientos()
        {
            return await repositorio.GetWithTratamientos();
        }
        [HttpGet("ConPresupuestos")]
        public async Task<ActionResult<List<Paciente>>> GetWithPresupuestos()
        {
            return await repositorio.GetWithPresupuestos();
        }

        [HttpGet("Full")]
        public async Task<ActionResult<List<Paciente>>> GetAll()
        {
            return await repositorio.FullGetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Paciente>> Get(int id)
        {
            Paciente? entidadReg = await repositorio.FullGetById(id);

            if (entidadReg == null)
            {
                return NotFound();
            }

            return entidadReg;
        }

        [HttpGet("NombreCompleto")]
        public async Task<ActionResult<List<Paciente>>> GetByName(string name)
        {
            var entidadReg = await repositorio.GetByName(name);
            if (entidadReg == null)
            {
                return NotFound();
            }

            return entidadReg;
            }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PacienteDTO entidadDTO)
        {
            var entidad = mapper.Map<Paciente>(entidadDTO);
            entidad.Historial_TratamientosOd = new List<TratamientoOd>();
            entidad.Historial_Presupuestos = new List<Presupuesto>();
            return await repositorio.Insert(entidad);
        }

        [HttpPut("{id:int}")] //api/Pacientes/*Id del paciente
        public async Task<ActionResult> Put(int id, [FromBody] PacienteDTO entidadDTO)
        {
            Paciente? entidad = await repositorio.SelectById(id);
            if (entidad == null)
            {
                return NotFound($"No se encontró paciente correspondiente a la ID: {id}.");
            }
            entidad = mapper.Map<Paciente>(entidadDTO);
            entidad.Id = id;

            try
            {
                if (await repositorio.Update(id, entidad))
                {
                    return Ok($"El paciente de ID {id} ha sido modificado correctamente.");
                }
                else
                {
                    return BadRequest("Algo salió mal.");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("CambioEstado/{id:int}")] //api/Pacientes/CambioEstado/*Id del Paciente*
        public async Task<ActionResult> PutChangeState(int id)
        {
            bool flag;
            Paciente? entidadReg;
            string result;
            try
            {
                flag = await repositorio.ChangeState(id);
                entidadReg = await repositorio.SelectById(id);
            }
            catch
            {
                return BadRequest();
            }

            if (!flag)
            {
                return NotFound("No existe el paciente buscado");
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
                return Ok($"El paciente '{entidadReg.NombreCompleto}' ahora está {result}.");
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
                return BadRequest($"El paciente a borrar (Id: {id}) no existe.");
            }
            
            if(await repositorio.Drop(id))
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
