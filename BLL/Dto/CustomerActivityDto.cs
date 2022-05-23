using System;

namespace BLL.Dto
{
    public class CustomerActivityDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal OrderSum { get; set; }
    }
}
