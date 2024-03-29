using ApiBlog.Data;
using ApiBlog.Modelos;
using ApiBlog.Modelos.DTO;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiBlog.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private readonly IConfiguration _config;
        private string claveSecreta;

        public UsuarioRepositorio(ApplicationDbContext bd,IConfiguration config)
        {
            _bd = bd;
            _config = config;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }

        //------------------------------------------------------------------------------------
        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuarios.FirstOrDefault(c => c.Id == usuarioId);
        }

        //------------------------------------------------------------------------------------
        public ICollection<Usuario> GetUsuarios()
        {            
            return _bd.Usuarios.OrderBy(c => c.Id).ToList();
        }

        //------------------------------------------------------------------------------------
        public bool IsUniqueUser(string usuario)
        {
            bool valor = _bd.Usuarios.Any(c => c.NombreUsuario.ToLower().Trim() == usuario.ToLower().Trim());
            return valor;
        }

        //------------------------------------------------------------------------------------
        public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDTO.Password);
            var usuario = _bd.Usuarios.FirstOrDefault(u => u.NombreUsuario.ToLower() == usuarioLoginDTO.NombreUsuario.ToLower() && usuarioLoginDTO.Password == passwordEncriptado);
            if(usuario == null)
            {
                return new UsuarioLoginRespuestaDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDTO usuarioLoginRespuestaDTO = new UsuarioLoginRespuestaDTO()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRespuestaDTO;
        }

        
        //------------------------------------------------------------------------------------
        public async Task<Usuario> Registro(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var passwordEncriptado = obtenermd5(usuarioRegistroDTO.Password);

            Usuario usuario = new Usuario()
            {
                NombreUsuario = usuarioRegistroDTO.NombreUsuario,
                Nombre = usuarioRegistroDTO.Nombre,
                Email = usuarioRegistroDTO.Email,
                Password = usuarioRegistroDTO.Password,
            };

            _bd.Usuarios.Add(usuario);
            usuario.Password = passwordEncriptado;
            await _bd.SaveChangesAsync();
            return usuario;
        }

        //------------------------------------------------------------------------------------
        private string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
