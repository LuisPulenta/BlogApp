using ApiBlog.Modelos;
using ApiBlog.Modelos.DTO;
using AutoMapper;

namespace ApiBlog.Mappers
{
    public class BlogMapper:Profile
    {
        public BlogMapper()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Post, PostActualizarDTO>().ReverseMap();
            CreateMap<Post, PostCrearDTO>().ReverseMap();
        }
    }
}
