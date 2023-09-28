using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class ProductResponseModel
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
    }
}
