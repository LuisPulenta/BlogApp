using ClienteBlazorWASM.Helpers;
using ClienteBlazorWASM.Modelos;
using ClienteBlazorWASM.Servicios.IServicio;
using Newtonsoft.Json;
using System.Text;

namespace ClienteBlazorWASM.Servicios
{
    public class PostsServicio : IPostsServicio
    {
        private readonly HttpClient _cliente;

        public PostsServicio(HttpClient cliente)
        {
            _cliente = cliente;
        }

        //--------------------------------------------------------------------------------------
        public async Task<Post> ActualizarPost(int postId, Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PatchAsync($"{Inicializar.UrlBaseApi}api/posts/{postId}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        //--------------------------------------------------------------------------------------
        public async Task<Post> CrearPost(Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/posts", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        //--------------------------------------------------------------------------------------
        public async Task<bool> EliminarPost(int postId)
        {
            var response = await _cliente.DeleteAsync($"{Inicializar.UrlBaseApi}api/posts/{postId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        //--------------------------------------------------------------------------------------
        public async Task<Post> GetPost(int postId)
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseApi}api/posts/{postId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<Post>(content);
                return post;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        //--------------------------------------------------------------------------------------
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseApi}api/posts");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
            return posts;
        }

        Task<Post> IPostsServicio.EliminarPost(int postId)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------------
        public async Task<string> SubidaImagen(MultipartFormDataContent content)
        {
            var postResult = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imagenPost = Path.Combine($"{Inicializar.UrlBaseApi}", postContent);
                return imagenPost;
            }
        }
    }
}