using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreUsuario { get; set; } = null!;
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; } = null!;
    }
}