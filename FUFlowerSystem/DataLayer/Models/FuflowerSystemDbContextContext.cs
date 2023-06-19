using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Models;

public partial class FuflowerSystemDbContextContext : DbContext
{
    public FuflowerSystemDbContextContext()
    {
    }

    public FuflowerSystemDbContextContext(DbContextOptions<FuflowerSystemDbContextContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<FlowerBouquet> FlowerBouquets { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("FUFlowerSystemDbContext"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.CustomerId)
            .UseIdentityColumn().HasColumnName("CustomerId").ValueGeneratedOnAdd();

            entity.Property(e => e.Bithday).HasColumnType("date");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<FlowerBouquet>(entity =>
        {
            entity.ToTable("FlowerBouquet");

            entity.HasKey(e => e.FlowerBouquetId);
            entity.Property(e => e.FlowerBouquetId)
            .UseIdentityColumn().HasColumnName("FlowerBouquetId").ValueGeneratedOnAdd();

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.FlowerBouquetName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.FlowerBouquets)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_FlowerBouquet_Category");

            entity.HasOne(d => d.Supplier).WithMany(p => p.FlowerBouquets)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_FlowerBouquet_Supplier");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.OrderId)
            .UseIdentityColumn().HasColumnName("OrderId").ValueGeneratedOnAdd();

            entity.Property(e => e.DeliveryDate).HasColumnType("date");
            entity.Property(e => e.DeliveryTo).HasMaxLength(250);
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.Total).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.FlowerBouquetId });

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Discount).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Price).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.FlowerBouquet).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.FlowerBouquetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_FlowerBouquet");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.SupplierName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
