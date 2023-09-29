using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class ParentCategoryRequestModel
    {
        [Required(ErrorMessage = "ParentCategoryName is required")]
        public string? ParentCategoryName { get; set; }
    }
}
