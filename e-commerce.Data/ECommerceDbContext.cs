using e_commerce.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> context) : base(context)
        {

        }

        public virtual DbSet<UserEntities> Users { get; set; }
        public virtual DbSet<ProductEntities> Products { get; set; }
        public virtual DbSet<CategoryEntities> Categories { get; set; }
        public virtual DbSet<ParentCategoryEntities> ParentCategories { get; set; }
    }
}
