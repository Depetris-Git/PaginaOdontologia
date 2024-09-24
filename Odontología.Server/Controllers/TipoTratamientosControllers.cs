using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data;
using Odontología.DB.Data.Entity;
using Odontología.Server.Repositorio;
using Odontología.Shared.DTO;
using System.IO.Pipelines;

namespace Odontología.Server.Controllers
{
    [ApiController]
    [Route("api/TipoTratamientos")]
    public class TipoTratamientosControllers : ControllerBase
    {
        private readonly ITipoTratamientoRepositorio repositorio;
        private readonly IMapper mapper;
        public TipoTratamientosControllers(ITipoTratamientoRepositorio repositorio,
                                            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<TipoTratamiento>>> Get()
        {
            return await repositorio.Select();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoTratamiento>> Get(int id)
        {
           TipoTratamiento? entidadReg = await repositorio.SelectById(id);

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
        public async Task<ActionResult<List<TipoTratamiento>>> GetOnlyActive()
        {
            return await repositorio.SelectActive();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] TipoTratamientoDTO entidadDTO)
        {
            var entidad = mapper.Map<TipoTratamiento>(entidadDTO);
            try
            {
              return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("CambioEstado/{id:int}")] //api/TipoTratamientos/CambioEstado/*Id del TipoTratamiento*
        public async Task<ActionResult> PutChangeState(int id)
        {
            string result;
            var flag = await repositorio.ChangeState(id);
            TipoTratamiento? entidadReg = await repositorio.SelectById(id);

            if (flag == false)
            {
                return NotFound("No existe el Tipo de Tratamiento buscado");
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
                return Ok($"El tipo de tratamiento '{entidadReg.Nombre}' ahora está {result}."); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id:int}")] //api/TipoTratamientos/*Id del TipoTratamiento*
        public async Task<ActionResult> Put(int id, [FromBody] TipoTratamientoDTO entidadDTO)
        {
            TipoTratamiento? entidad = await repositorio.SelectById(id);
            if (entidad == null) 
            {
                return NotFound($"No se encontró el tipo de tratamiento correspondiente a la ID: {id}.");
            }
            entidad = mapper.Map<TipoTratamiento>(entidadDTO);
            entidad.Id = id;

            try
            {
                if(await repositorio.Update(id, entidad))
                {
                    return Ok($"El tipo de tratamiento de ID: {id} ha sido modificado correctamente.");
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await repositorio.Exist(id);
            if (!existe)
            {
                return BadRequest($"El tipo de tratamiento a borrar (Id: {id}) no existe");
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
