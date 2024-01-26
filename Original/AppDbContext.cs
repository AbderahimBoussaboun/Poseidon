using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Poseidon.Api.Data
{
    public class AppDbContext : DbContext
    {
        // DbSet para la entidad Dato
        public DbSet<Dato> Datos { get; set; }

        // Constructor que recibe opciones de DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // M�todo para configurar el modelo de datos (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Puedes agregar configuraciones adicionales aqu� si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}
