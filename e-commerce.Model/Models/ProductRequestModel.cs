using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class ProductRequestModel
    {
        [Required(ErrorMessage = "ProductName is required")]
        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Color is required")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Size is required")]
        public string? Size { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "CategoryID is required")]
        public int? CategoryID { get; set; }
    }
}
