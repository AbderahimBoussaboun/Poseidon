using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Balancer> Balancers { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<ComponetType> ComponetTypes { get; set; }

    public virtual DbSet<Environment> Environments { get; set; }

    public virtual DbSet<Infraestructure> Infraestructures { get; set; }

    public virtual DbSet<Irule> Irules { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Node> Nodes { get; set; }

    public virtual DbSet<NodePool> NodePools { get; set; }

    public virtual DbSet<Pool> Pools { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<ServerApplication> ServerApplications { get; set; }

    public virtual DbSet<SubApplication> SubApplications { get; set; }

    public virtual DbSet<Virtual> Virtuals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Prueba2;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Applications_ProductId");

            entity.Property(e => e.ApplicationId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Product).WithMany(p => p.Applications).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Balancer>(entity =>
        {
            entity.Property(e => e.BalancerId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasIndex(e => e.BalancerId, "IX_Components_BalancerId");

            entity.HasIndex(e => e.ComponentTypeId, "IX_Components_ComponentTypeId");

            entity.HasIndex(e => e.SubApplicationId, "IX_Components_SubApplicationId");

            entity.Property(e => e.ComponentId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Balancer).WithMany(p => p.Components).HasForeignKey(d => d.BalancerId);

            entity.HasOne(d => d.ComponentType).WithMany(p => p.Components).HasForeignKey(d => d.ComponentTypeId);

            entity.HasOne(d => d.SubApplication).WithMany(p => p.Components)
                .HasForeignKey(d => d.SubApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ComponetType>(entity =>
        {
            entity.HasKey(e => e.ComponentTypeId);

            entity.Property(e => e.ComponentTypeId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Environment>(entity =>
        {
            entity.Property(e => e.EnvironmentId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Infraestructure>(entity =>
        {
            entity.Property(e => e.InfraestructureId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Irule>(entity =>
        {
            entity.Property(e => e.IruleId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Rule).WithMany(p => p.Irules)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.Property(e => e.MonitorId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DefaultsFrom).HasColumnName("Defaults_from");
            entity.Property(e => e.Get).HasColumnName("get");
            entity.Property(e => e.IpDscp).HasColumnName("IP_DSCP");
            entity.Property(e => e.Recv).HasColumnName("RECV");
            entity.Property(e => e.RecvDisable).HasColumnName("RECV_disable");
            entity.Property(e => e.Send).HasColumnName("SEND");
            entity.Property(e => e.SslProfile).HasColumnName("ssl_profile");
            entity.Property(e => e.TimeUntilUp).HasColumnName("time_until_up");
            entity.Property(e => e.Timeout).HasColumnName("timeout");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Node>(entity =>
        {
            entity.Property(e => e.NodeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ip).HasColumnName("IP");
        });

        modelBuilder.Entity<NodePool>(entity =>
        {
            entity.HasKey(e => new { e.NodeId, e.PoolId });

            entity.ToTable("NodePool");

            entity.HasOne(d => d.Node).WithMany(p => p.NodePools)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Pool).WithMany(p => p.NodePools)
                .HasForeignKey(d => d.PoolId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Pool>(entity =>
        {
            entity.Property(e => e.PoolId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Monitor).WithMany(p => p.Pools)
                .HasForeignKey(d => d.MonitorId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.ServerId, "IX_Roles_ServerId");

            entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Server).WithMany(p => p.Roles).HasForeignKey(d => d.ServerId);
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.Property(e => e.RuleId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Virtual).WithMany(p => p.Rules)
                .HasForeignKey(d => d.VirtualId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasIndex(e => e.EnvironmentId, "IX_Servers_EnvironmentId");

            entity.HasIndex(e => e.ProductId, "IX_Servers_ProductId");

            entity.Property(e => e.ServerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Os).HasColumnName("OS");

            entity.HasOne(d => d.Environment).WithMany(p => p.Servers).HasForeignKey(d => d.EnvironmentId);

            entity.HasOne(d => d.Product).WithMany(p => p.Servers).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<ServerApplication>(entity =>
        {
            entity.HasIndex(e => e.ComponentId, "IX_ServerApplications_ComponentId")
                .IsUnique()
                .HasFilter("([ComponentId] IS NOT NULL)");

            entity.HasIndex(e => e.RoleId, "IX_ServerApplications_RoleId");

            entity.Property(e => e.ServerApplicationId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Component).WithOne(p => p.ServerApplication).HasForeignKey<ServerApplication>(d => d.ComponentId);

            entity.HasOne(d => d.Role).WithMany(p => p.ServerApplications).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<SubApplication>(entity =>
        {
            entity.HasIndex(e => e.ApplicationId, "IX_SubApplications_ApplicationId");

            entity.Property(e => e.SubApplicationId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Application).WithMany(p => p.SubApplications).HasForeignKey(d => d.ApplicationId);
        });

        modelBuilder.Entity<Virtual>(entity =>
        {
            entity.Property(e => e.VirtualId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Pool).WithMany(p => p.Virtuals).HasForeignKey(d => d.PoolId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
