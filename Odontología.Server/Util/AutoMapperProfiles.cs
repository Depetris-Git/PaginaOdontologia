using AutoMapper;
using Odontología.DB.Data.Entity;
using Odontología.Shared.DTO;

namespace Odontología.Server.Util
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PacienteDTO, Paciente>();
            CreateMap<PagoDTO, Pago>();
            CreateMap<PresupuestoDTO, Presupuesto>();
            CreateMap<TipoTratamientoDTO, TipoTratamiento>();
            CreateMap<TratamientoOdDTO, TratamientoOd>();
        }
    }
}
