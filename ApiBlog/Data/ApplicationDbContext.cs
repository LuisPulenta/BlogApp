using ApiBlog.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Agregar modelos
        public DbSet<Post> Posts { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
