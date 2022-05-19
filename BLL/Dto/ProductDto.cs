using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<int> OrderDetailsIds { get; set; }
    }
}
