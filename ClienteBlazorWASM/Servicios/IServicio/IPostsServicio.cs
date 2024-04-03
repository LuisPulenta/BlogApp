using ClienteBlazorWASM.Modelos;

namespace ClienteBlazorWASM.Servicios.IServicio
{
    public interface IPostsServicio
    {
        public Task<IEnumerable<Post>> GetPosts();

        public Task<Post> GetPost(int postId);
        
        public Task<Post> CrearPost(Post post);

        public Task<Post> ActualizarPost(int postId, Post post);

        public Task<bool> EliminarPost(int postId);

        public Task<string> SubidaImagen(MultipartFormDataContent content);
    }
}
