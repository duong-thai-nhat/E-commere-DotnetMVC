using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Model.Entities
{
    public class ParentCategoryEntities
    {
        [Key]
        public int ParentCategoryId { get; set; }

        public string? ParentCategoryName { get; set; }
    }
}
