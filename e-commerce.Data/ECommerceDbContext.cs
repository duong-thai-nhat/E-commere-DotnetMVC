using e_commerce.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace e_commerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> context) : base(context)
        {

        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>()
        //        .HasOne<Grade>(s => s.Grade)
        //        .WithMany(g => g.Students)
        //        .HasForeignKey(s => s.CurrentGradeId);
        }
        public virtual DbSet<UserEntities> Users { get; set; }
        public virtual DbSet<ProductEntities> Products { get; set; }
        public virtual DbSet<CategoryEntities> Categories { get; set; }
        public virtual DbSet<ParentCategoryEntities> ParentCategories { get; set; }
        public virtual DbSet<UserRoleEntities> UserRoles { get; set; }
        public virtual DbSet<CartEntities> Carts { get; set; }
    }
}
