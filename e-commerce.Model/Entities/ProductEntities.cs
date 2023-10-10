using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Model.Entities
{
    public class ProductEntities
    {
        [Key]
        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public string? Color { get; set; }

        public string? Size { get; set; }

        public int Price { get; set; }

        public string? Description { get; set;}

        [ForeignKey("CategoryId")]
        public CategoryEntities Category { get; set; }
    }
}
