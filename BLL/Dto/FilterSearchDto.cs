using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class FilterSearchDto
    {
        public Guid? CategoryId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
