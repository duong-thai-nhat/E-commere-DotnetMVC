using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class CategoryRequestModel
    {
        [Required(ErrorMessage = "CategoryName is required")]
        public string? CategoryName { get; set; }
    }
}
