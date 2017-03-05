using Microsoft.EntityFrameworkCore;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data
{
    public class RefactorMeDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOption> ProductOptions { get; set; }

        public RefactorMeDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //EntityTypeConfiguration<T> is not supported in EF Core yet. Need to find a workaround.
            modelBuilder.Entity<Product>()
                .ToTable("Product");

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);;

            modelBuilder.Entity<Product>()
                .Property(p => p.DeliveryPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            modelBuilder.Entity<ProductOption>()
                .ToTable("ProductOption");

            modelBuilder.Entity<ProductOption>()
                .HasKey(po => po.Id);

            modelBuilder.Entity<ProductOption>()
                .Property(po => po.ProductId)
                .IsRequired();

            modelBuilder.Entity<ProductOption>()
                .Property(po => po.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ProductOption>()
                .Property(po => po.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<ProductOption>()
                .HasOne(p => p.Product)
                .WithMany(po => po.ProductOptions)
                .HasForeignKey(po => po.ProductId);      
        }
    }
}