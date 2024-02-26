using ACME.Helper;
using ACME.Models;
using AutoMapper;
using ACME.Entities;

namespace ACME.Profiles;
public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Usuario, UsuarioDto>();
		CreateMap<UsuarioDto, Usuario>();
        CreateMap<Encuesta, EncuestaDto>();
        CreateMap<EncuestaDto, Encuesta>();
        CreateMap<CampoEncuesta, CampoEncuestaDto>();
        CreateMap<CampoEncuestaDto, CampoEncuesta>();
		CreateMap<ResultadoEncuesta, ResultadoEncuestaDto>();
		CreateMap<ResultadoEncuestaDto, ResultadoEncuesta>();

		CreateMap<RecordList<Usuario>, UsuariosDto>(MemberList.Destination)
            .ForMember(x => x.Usuarios, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Encuesta>, EncuestasDto>(MemberList.Destination)
            .ForMember(x => x.Encuestas, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<CampoEncuesta>, CamposEncuestasDto>(MemberList.Destination)
            .ForMember(x => x.CamposEncuestas, opt => opt.MapFrom(src => src.Data));

		CreateMap<RecordList<ResultadoEncuesta>, ResultadosEncuestasDto>(MemberList.Destination)
			.ForMember(x => x.ResultadosEncuestas, opt => opt.MapFrom(src => src.Data));
	}
}
