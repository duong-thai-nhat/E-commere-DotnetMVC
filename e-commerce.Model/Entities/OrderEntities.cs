using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Entities
{
    public class OrderEntities
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public UserEntities? User { get; set; }

        public List<OrderDetailEntities>? OrderDetailEntities { get; set; }
    }
}