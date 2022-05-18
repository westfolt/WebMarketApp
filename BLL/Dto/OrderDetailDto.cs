using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal DiscountProductPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
