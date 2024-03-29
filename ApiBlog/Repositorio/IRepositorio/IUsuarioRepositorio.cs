using ApiBlog.Modelos;
using ApiBlog.Modelos.DTO;

namespace ApiBlog.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();

        Usuario GetUsuario(int usuarioId);

        bool IsUniqueUser(string usuario);

        Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO) ;

        Task<Usuario> Registro(UsuarioRegistroDTO usuarioRegistroDTO);
    }
}
