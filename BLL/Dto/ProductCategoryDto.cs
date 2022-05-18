using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<int> ProductsIds { get; set; }
    }
}
