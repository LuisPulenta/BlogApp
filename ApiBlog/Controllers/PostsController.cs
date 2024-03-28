using ApiBlog.Modelos;
using ApiBlog.Modelos.DTO;
using ApiBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlog.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepositorio _postRepo;
        private readonly IMapper _mapper;

        public PostsController(IPostRepositorio postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        //---------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPosts()
        {
            var listaPosts = _postRepo.GetPosts();
            var listaPostsDTO = new List<PostDTO>();
            foreach (var lista in listaPosts)
            {
                listaPostsDTO.Add(_mapper.Map<PostDTO>(lista));
            }
            return Ok(listaPostsDTO);
        }

        //---------------------------------------------------------------------------------------------
        [HttpGet("{postId:int}",Name ="GetPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int postId)
        {
            var itemPost = _postRepo.GetPost(postId);

            if (itemPost == null)
            {
                return NotFound();
            }

            var itemPostDTO = _mapper.Map<PostDTO>(itemPost);

            return Ok(itemPostDTO);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        [ProducesResponseType(201,Type=typeof(PostCrearDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public IActionResult CrearPost([FromBody] PostCrearDTO crearPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearPostDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_postRepo.ExistePost(crearPostDTO.Titulo))
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404,ModelState);
            }
            var post = _mapper.Map<Post>(crearPostDTO);

            if (!_postRepo.CrearPost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {post.Titulo}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPost", new { postId = post.Id }, post);
        }

        //---------------------------------------------------------------------------------------------
        [HttpPatch("{postId:int}", Name = "ActualizarPatchPost")]
        [ProducesResponseType(201, Type = typeof(PostCrearDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ActualizarPatchPost(int postId,[FromBody] PostActualizarDTO actualizarPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (actualizarPostDTO == null || postId != actualizarPostDTO.Id)
            {
                return BadRequest(ModelState);
            }

            if (_postRepo.ExistePost(actualizarPostDTO.Titulo))
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404, ModelState);
            }
            var post = _mapper.Map<Post>(actualizarPostDTO);

            if (!_postRepo.ActualizarPost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {post.Titulo}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //---------------------------------------------------------------------------------------------
        [HttpDelete("{postId:int}", Name = "BorrarPost")]
        [ProducesResponseType(201, Type = typeof(PostCrearDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult BorrarPost(int postId)
        {
            if (!_postRepo.ExistePost(postId))
            {
                return NotFound();
            }
            var post = _postRepo.GetPost(postId);

            if (!_postRepo.BorrarPost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {post.Titulo}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
