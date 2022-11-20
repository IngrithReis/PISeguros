using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PISeguros.API.Models.Entities;

namespace PISeguros.API.Database
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Corretor> Corretores { get; set; }
        public DbSet<Seguro> Seguros { get; set; }
        public DbSet<Proponente> Proponentes { get; set; }
        public DbSet<Segurado> Segurados { get; set; }
        public DbSet<Dependente> Dependentes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
