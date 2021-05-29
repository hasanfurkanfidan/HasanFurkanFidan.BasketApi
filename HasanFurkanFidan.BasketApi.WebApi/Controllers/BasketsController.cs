using HasanFurkanFidan.BasketApi.WebApi.Dtos;
using HasanFurkanFidan.BasketApi.WebApi.Services;
using HasanFurkanFidan.UdemyCourse.SHARED.ControllerBases;
using HasanFurkanFidan.UdemyCourse.SHARED.Dtos;
using HasanFurkanFidan.UdemyCourse.SHARED.IdentityServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IBasketService _basketService;
        public BasketsController(IIdentityService identityService, IBasketService basketService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var userId = _identityService.GetUserId();
            var response = await _basketService.GetBasketAsync(userId);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            basketDto.UserId = _identityService.GetUserId();
            var response = await _basketService.AddOrUpdateAsync(basketDto);
            return CreateActionResultInstance(response);
        }

    }
}
