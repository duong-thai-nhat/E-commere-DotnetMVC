using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Entities
{
    public class CartEntities
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("UserId")]
        public UserEntities? User { get; set; }

        [ForeignKey("ProductId")]
        public ProductEntities? Product { get; set; }
    }
}
