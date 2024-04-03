using System.ComponentModel.DataAnnotations;

namespace ClienteBlazorWASM.Modelos
{
    public class UsuarioRegistro
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; } = null!;
    }
}
