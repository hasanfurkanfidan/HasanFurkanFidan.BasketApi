using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCount { get; set; }
        public decimal Price { get=>BasketItemDtos.Sum(x=>x.Price*x.Quantity);}
        public List<BasketItemDto> BasketItemDtos { get; set; }
    }
}
