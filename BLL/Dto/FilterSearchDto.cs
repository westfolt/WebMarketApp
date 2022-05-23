using System;

namespace BLL.Dto
{
    public class FilterSearchDto
    {
        public Guid? CategoryId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
