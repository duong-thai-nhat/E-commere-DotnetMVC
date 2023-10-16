using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class OrderRequestModel
    {
        [Required(ErrorMessage = "Please add OrderDate to the request.")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
