﻿using System.ComponentModel.DataAnnotations;

namespace ClienteBlazorWASM.Modelos
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Titulo { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; } = null!;
        public string? RutaImagen { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Etiquetas { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
    }
}
