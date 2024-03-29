namespace ApiBlog.Modelos.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Email { get; set; }
    }
}
