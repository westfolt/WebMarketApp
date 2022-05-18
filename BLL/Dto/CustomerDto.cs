using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DiscountPercent { get; set; }
        public ICollection<int> OrdersIds { get; set; }
    }
}
