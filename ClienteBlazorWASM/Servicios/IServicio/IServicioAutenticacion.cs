using ClienteBlazorWASM.Modelos;

namespace ClienteBlazorWASM.Servicios.IServicio
{
    public interface IServicioAutenticacion
    {
        Task<RespuestaRegistro> RegistrarUsuario(UsuarioRegistro usuarioParaRegistro);
        Task<RespuestaAutenticacion> Acceder(UsuarioAutenticacion usuarioDesdeAutenticacion);
        Task Salir();
    }
}
