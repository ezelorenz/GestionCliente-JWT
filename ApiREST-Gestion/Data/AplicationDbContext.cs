using ApiREST_Gestion.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiREST_Gestion.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options):base(options)
        {

        }
        public DbSet<Cliente>Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
