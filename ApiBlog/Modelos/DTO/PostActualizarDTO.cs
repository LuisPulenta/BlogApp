using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Modelos.DTO
{
    public class PostActualizarDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Titulo { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; } = null!;
        public string? RutaImagen { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Etiquetas { get; set; } = null!;
        public DateTime? FechaActualizacion { get; set; }
    }
}
