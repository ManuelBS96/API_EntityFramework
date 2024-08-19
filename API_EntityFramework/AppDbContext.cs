using API_EntityFramework.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API_EntityFramework
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions options ): base(options)
        {

        }
        public DbSet<Autor> Autores { get; set; }

        public DbSet<Libro> Libros { get; set; }
    }
}
