using System.ComponentModel.DataAnnotations;

namespace e_commerce.Model.Entities
{
    public class CategoryEntities
    {
        [Key]
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; }

        public int ParentId { get; set; }
    }
}
