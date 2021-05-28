using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
    }
}
