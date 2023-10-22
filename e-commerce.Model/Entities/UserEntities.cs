using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("RoleId")]
        public int RoleId {get; set;}

        public UserRoleEntities? Role { get; set; }

        public List<CartEntities>? CartEntities { get; set; }

        public List<OrderEntities>? OrderEntities { get; set; }
    }
}
