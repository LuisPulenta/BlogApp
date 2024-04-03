using ApiBlog.Modelos;
using ApiBlog.Modelos.DTO;
using AutoMapper;

namespace ApiBlog.Mappers
{
    public class UsuarioMapper:Profile
    {
        public UsuarioMapper()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioRegistroDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginRespuestaDTO>().ReverseMap();
        }
    }
}
