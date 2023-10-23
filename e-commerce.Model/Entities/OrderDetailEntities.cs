using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Entities
{
    public class OrderDetailEntities
    {
        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Note { get; set; }

        public OrderEntities? Order { get; set; }

        public ProductEntities? Product { get; set; }
    }
}
