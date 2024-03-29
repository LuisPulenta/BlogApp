using ApiBlog.Modelos;
using ApiBlog.Modelos.DTO;
using ApiBlog.Repositorio;
using ApiBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBlog.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usRepo;
        private readonly IMapper _mapper;

        protected RespuestasAPI _respuestasAPI;

        public UsuariosController(IUsuarioRepositorio usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            this._respuestasAPI = new();
        }

        //---------------------------------------------------------------------------------------------
        [HttpPost("Registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDTO usuarioRegistroDTO)
        {
            bool validarNombreUsuarioUnico = _usRepo.IsUniqueUser(usuarioRegistroDTO.NombreUsuario);
            if (validarNombreUsuarioUnico)
            {
                _respuestasAPI.StatusCode=HttpStatusCode.BadRequest;
                _respuestasAPI.IsScuccess = false;
                _respuestasAPI.ErrorMessages.Add("El nombre de usuario ya existe");
                return BadRequest(_respuestasAPI);
            }

            var usuario = await _usRepo.Registro(usuarioRegistroDTO);
            if (usuario == null)
            {
                _respuestasAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestasAPI.IsScuccess = false;
                _respuestasAPI.ErrorMessages.Add("Error en el registro");
                return BadRequest(_respuestasAPI);
            }
            _respuestasAPI.StatusCode = HttpStatusCode.OK;
            _respuestasAPI.IsScuccess = true;
            return Ok(_respuestasAPI);
        }

        //---------------------------------------------------------------------------------------------
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            var respuestaLogin = await _usRepo.Login(usuarioLoginDTO);
            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestasAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestasAPI.IsScuccess = false;
                _respuestasAPI.ErrorMessages.Add("El nombre del usuario o password son incorrectos");
                return BadRequest(_respuestasAPI);
            }
            _respuestasAPI.StatusCode = HttpStatusCode.OK;
            _respuestasAPI.IsScuccess = true;
            _respuestasAPI.Result = respuestaLogin;
            return Ok(_respuestasAPI);
        }

        //---------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _usRepo.GetUsuarios();
            var listaUsuariosDTO = new List<UsuarioDTO>();
            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDTO.Add(_mapper.Map<UsuarioDTO>(lista));
            }
            return Ok(listaUsuariosDTO);
        }

        //---------------------------------------------------------------------------------------------
        [HttpGet("{usuarioId:int}",Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _usRepo.GetUsuario(usuarioId);

            if (itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDTO = _mapper.Map<UsuarioDTO>(itemUsuario);

            return Ok(itemUsuarioDTO);
        }
    }
}
