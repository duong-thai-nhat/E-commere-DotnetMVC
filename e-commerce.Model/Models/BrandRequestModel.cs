using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class BrandRequestModel
    {
        [Required(ErrorMessage = "BrandName is required")]
        public string? BrandName { get; set; }
    }
}
