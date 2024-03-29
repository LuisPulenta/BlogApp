namespace ApiBlog.Modelos.DTO
{
    public class UsuarioLoginRespuestaDTO
    {
        public Usuario? Usuario { get; set; }

        public string Token { get; set; } = null!;
    }
}
