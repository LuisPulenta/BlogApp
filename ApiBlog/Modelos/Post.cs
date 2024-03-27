using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Modelos
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        public string? RutaImagen { get; set; }
        [Required]
        public string Etiquetas { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }=DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
    }
}
