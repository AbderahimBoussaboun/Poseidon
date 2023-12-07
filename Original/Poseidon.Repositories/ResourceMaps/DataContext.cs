using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Entities.ResourceMaps.Balancers;
using Poseidon.Entities.ResourceMaps.Products;
using Poseidon.Entities.ResourceMaps.Servers;
using System.Reflection;


namespace Poseidon.Repositories.ResourceMaps
{
    public class DataContext : DbContext
    {

        protected readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options) 
        { _configuration = configuration; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            //Configurate all the class that extends from IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            //new ServerConfiguration().Configure(modelBuilder.Entity<Server>());
            base.OnModelCreating(modelBuilder);

            
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Balancer> Balancers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<SubApplication> SubApplications { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ServerApplication> ServerApplications { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<ComponentType> ComponetTypes { get; set; }
        public DbSet<Infraestructure> Infraestructures { get; set; }
    }
}
