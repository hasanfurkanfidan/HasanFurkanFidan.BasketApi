using HasanFurkanFidan.BasketApi.WebApi.Dtos;
using HasanFurkanFidan.UdemyCourse.SHARED.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasketAsync(string userId);
        Task<Response<bool>> AddOrUpdateAsync(BasketDto basketDto);
        Task<Response<bool>> DeleteAsnyc(string userId);
    }
}
