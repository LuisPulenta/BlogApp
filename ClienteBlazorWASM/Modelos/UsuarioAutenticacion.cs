using System.ComponentModel.DataAnnotations;

namespace ClienteBlazorWASM.Modelos
{
    public class UsuarioAutenticacion
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; } = null!;
    }
}
