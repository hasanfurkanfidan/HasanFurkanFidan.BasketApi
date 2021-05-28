using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasanFurkanFidan.UdemyCourse.SHARED.IdentityServices
{
    public class IdentityManager : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public  string GetUserId()
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.Where(p => p.Type == "sub").FirstOrDefault().Value;
            return claim;
        }
    }
}
