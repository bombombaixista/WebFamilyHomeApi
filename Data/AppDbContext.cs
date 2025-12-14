using Microsoft.EntityFrameworkCore;
using WebFamilyHomeApi.Models;

namespace WebFamilyHomeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
    }
}
