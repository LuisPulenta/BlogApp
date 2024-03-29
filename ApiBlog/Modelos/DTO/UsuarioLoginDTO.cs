using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Modelos.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage ="El usuario es obligatorio")]
        public string NombreUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; } = null!;
    }
}
