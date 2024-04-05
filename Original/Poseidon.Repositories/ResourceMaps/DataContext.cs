using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Entities.ResourceMaps.Balancers;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Entities.ResourceMaps.Products;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Reflection;
using Environment = Poseidon.Entities.ResourceMaps.Servers.Environment;

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
                optionsBuilder.UseSqlServer(
       _configuration.GetConnectionString("DefaultConnection"),
       builder => builder.EnableRetryOnFailure())
                   
       .EnableSensitiveDataLogging()
       .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurate all the class that extends from IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //new ServerConfiguration().Configure(modelBuilder.Entity<Server>());
            base.OnModelCreating(modelBuilder);

            //A revisar....
            modelBuilder.Entity<Virtual>()
         .Property(v => v.Id)
         .HasColumnName("VirtualId");

            modelBuilder.Entity<NodePool>(entity =>
            {
                entity.HasKey(np => new { np.NodeId, np.PoolId });

                entity.HasOne(np => np.Node)
                    .WithMany(n => n.NodePools)
                    .HasForeignKey(np => np.NodeId);

                entity.HasOne(np => np.Pool)
                    .WithMany(p => p.NodePools)
                    .HasForeignKey(np => np.PoolId);

                entity.ToTable("NodePool");
            });

            modelBuilder.Entity<Node>()
        .Property(n => n.Id)
        .HasColumnName("NodeId");

            modelBuilder.Entity<Pool>()
        .Property(p => p.Id)
        .HasColumnName("PoolId");

            modelBuilder.Entity<Monitor>()
        .Property(m => m.Id)
        .HasColumnName("MonitorId");

            modelBuilder.Entity<Rule>()
        .Property(r => r.Id)
        .HasColumnName("RuleId");

            modelBuilder.Entity<Irule>()
        .Property(i => i.Id)
        .HasColumnName("IruleId");

            modelBuilder.Entity<NodePool>()
    .HasKey(v => new { v.NodeId, v.PoolId });


            // Configurar la relación entre Irule y Rule
            modelBuilder.Entity<Irule>()
                .HasOne(i => i.Rule) // Cada Irule tiene una propiedad Rule
                .WithMany(r => r.Irules) // Cada Rule tiene una colección de Irules
                .HasForeignKey(i => i.RuleId) // La clave foránea es RuleId
                .OnDelete(DeleteBehavior.Cascade); // Al eliminar un Rule, se eliminan los Irules relacionados

            // Configurar la relación entre Monitor y Pool
            modelBuilder.Entity<Monitor>()
                .HasMany(m => m.Pools) // Cada Monitor tiene una colección de Pools
                .WithOne(p => p.Monitor) // Cada Pool tiene una propiedad Monitor
                .HasForeignKey(p => p.MonitorId) // La clave foránea es MonitorId
                .OnDelete(DeleteBehavior.SetNull); // Al eliminar un Monitor, se establece a null la propiedad Monitor de los Pools relacionados

            // Configurar la relación entre Node y NodePool
            modelBuilder.Entity<Node>()
                .HasMany(n => n.NodePools) // Cada Node tiene una colección de NodePools
                .WithOne(np => np.Node) // Cada NodePool tiene una propiedad Node
                .HasForeignKey(np => np.NodeId) // La clave foránea es NodeId
                .OnDelete(DeleteBehavior.Cascade); // Al eliminar un Node, se eliminan los NodePools relacionados

            // Configurar la relación entre Pool y NodePool
            modelBuilder.Entity<Pool>()
                .HasMany(p => p.NodePools) // Cada Pool tiene una colección de NodePools
                .WithOne(np => np.Pool) // Cada NodePool tiene una propiedad Pool
                .HasForeignKey(np => np.PoolId) // La clave foránea es PoolId
                .OnDelete(DeleteBehavior.Cascade); // Al eliminar un Pool, se eliminan los NodePools relacionados

            // Configurar la relación entre Pool y Virtual
            modelBuilder.Entity<Pool>()
                .HasMany(p => p.Virtuals) // Cada Pool tiene una colección de Virtuals
                .WithOne(v => v.Pool) // Cada Virtual tiene una propiedad Pool
                .HasForeignKey(v => v.PoolId) // La clave foránea es PoolId
                .OnDelete(DeleteBehavior.SetNull); // Al eliminar un Pool, se establece a null la propiedad Pool de los Virtuals relacionados

            // Configurar la relación entre Rule y Virtual
            modelBuilder.Entity<Rule>()
                .HasOne(r => r.Virtual) // Cada Rule tiene una propiedad Virtual
                .WithMany(v => v.Rules) // Cada Virtual tiene una colección de Rules
                .HasForeignKey(r => r.VirtualId) // La clave foránea es VirtualId
                .OnDelete(DeleteBehavior.Cascade); // Al eliminar un Virtual, se eliminan los Rules relacionados



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
        public DbSet<Infrastructure> Infrastructures { get; set; }
        public DbSet<Virtual> Virtuals { get; set; }
        public DbSet<Pool> Pools { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Irule> IRules { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<NodePool> NodePools { get; set; }
        public DbSet<Monitor> Monitors { get; set; }


    }
}
