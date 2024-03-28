using ApiBlog.Data;
using ApiBlog.Modelos;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Repositorio
{
    public class PostRepositorio : IPostRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public PostRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        //-----------------------------------------------------------------------------------------------
        public bool ActualizarPost(Post post)
        {
            post.FechaActualizacion=DateTime.Now;
            var imagenDesdeBd = _bd.Posts.AsNoTracking().FirstOrDefault(c => c.Id == post.Id);
            if(post.RutaImagen == null )
            {
                post.RutaImagen = imagenDesdeBd.RutaImagen;
            }
            _bd.Posts.Update(post);
            return Guardar();
        }

        //-----------------------------------------------------------------------------------------------
        public bool BorrarPost(Post post)
        {
            _bd.Posts.Remove(post);
            return Guardar();
        }

        //-----------------------------------------------------------------------------------------------
        public bool CrearPost(Post post)
        {
            post.FechaCreacion = DateTime.Now;
            _bd.Posts.Add(post);
            return Guardar();
        }

        //-----------------------------------------------------------------------------------------------
        public bool ExistePost(string nombre)
        {
            bool valor = _bd.Posts.Any(c => c.Titulo.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        //-----------------------------------------------------------------------------------------------
        public bool ExistePost(int id)
        {
            bool valor = _bd.Posts.Any(c => c.Id == id);
            return valor;
        }

        //-----------------------------------------------------------------------------------------------
        public Post GetPost(int postId)
        {
            return _bd.Posts.FirstOrDefault(c => c.Id == postId);
        }

        //-----------------------------------------------------------------------------------------------
        public ICollection<Post> GetPosts()
        {
            return _bd.Posts.OrderBy(c => c.Id).ToList();
        }

        //-----------------------------------------------------------------------------------------------
        public bool Guardar()
        {
            return _bd.SaveChanges()>=0?true:false;
        }
    }
}