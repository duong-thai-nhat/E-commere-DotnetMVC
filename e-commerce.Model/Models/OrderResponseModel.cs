using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Models
{
    public class OrderResponseModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string? User {  get; set; }
    }
}
