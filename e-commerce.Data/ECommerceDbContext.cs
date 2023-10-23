using e_commerce.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> context) : base(context)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartEntities>().HasKey(pc => new { pc.ProductId, pc.UserId });
            modelBuilder.Entity<CartEntities>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartEntities)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Cart_Product_PM");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartEntities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Cart_User_PM");
            });

            modelBuilder.Entity<OrderDetailEntities>().HasKey(pc => new { pc.ProductId, pc.OrderId });
            modelBuilder.Entity<OrderDetailEntities>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetailEntities)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetail_Product_PM");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetailEntities)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order_PM");
            });

            // Cấu hình kiểu dữ liệu cho thuộc tính Price là decimal(10, 2)
            modelBuilder.Entity<ProductEntities>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<OrderDetailEntities>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(10, 2)");
        }

        public virtual DbSet<UserEntities> Users { get; set; }
        public virtual DbSet<ProductEntities> Products { get; set; }
        public virtual DbSet<CategoryEntities> Categories { get; set; }
        public virtual DbSet<ParentCategoryEntities> ParentCategories { get; set; }
        public virtual DbSet<UserRoleEntities> UserRoles { get; set; }
        public virtual DbSet<CartEntities> Carts { get; set; }
        public virtual DbSet<OrderEntities> Orders { get; set; }
        public virtual DbSet<OrderDetailEntities> OrderDetails { get; set; }
    }
}