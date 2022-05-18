using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class ProductCategory:BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
