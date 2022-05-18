using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class CustomerActivityDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal OrderSum { get; set; }
    }
}
