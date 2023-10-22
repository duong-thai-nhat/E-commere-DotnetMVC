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
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }

        public UserEntities? User { get; set; }

        public ProductEntities? Product { get; set; }
    }
}
