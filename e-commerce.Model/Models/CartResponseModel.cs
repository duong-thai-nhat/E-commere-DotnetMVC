using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class CartResponseModel
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? Product { get; set; }

        public string? User {  get; set; }
    }
}
