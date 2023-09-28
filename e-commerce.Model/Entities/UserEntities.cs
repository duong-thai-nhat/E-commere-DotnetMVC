using System.ComponentModel.DataAnnotations;

namespace e_commerce.Model.Entities
{
    public class UserEntities
    {
        [Key]
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? PassWord { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
