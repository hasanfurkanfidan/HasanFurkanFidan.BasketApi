using HasanFurkanFidan.BasketApi.WebApi.Dtos;
using HasanFurkanFidan.UdemyCourse.SHARED.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi.Services
{
    public class BasketManager : IBasketService
    {
        private readonly RedisManager _redisManager;
        public BasketManager(RedisManager redisManager)
        {
            _redisManager = redisManager;
        }
        public async Task<Response<bool>> AddOrUpdateAsync(BasketDto basketDto)
        {

            var status = await _redisManager.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            if (status)
            {
                return Response<bool>.Success(true, 200);
            }
            return Response<bool>.Fail("Redise ekleme hatası", 400);
        }

        public async Task<Response<bool>> DeleteAsnyc(string userId)
        {
            var existBasket = await _redisManager.GetDatabase().StringGetAsync(userId);
            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<bool>.Fail("", 404);

            }
            await _redisManager.GetDatabase().KeyDeleteAsync(userId);
            return Response<bool>.Success(true, 200);
        }

        public async Task<Response<BasketDto>> GetBasketAsync(string userId)
        {
            var existBasket = await _redisManager.GetDatabase().StringGetAsync(userId);
            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("", 404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }
    }
}
