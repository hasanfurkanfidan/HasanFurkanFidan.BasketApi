using HasanFurkanFidan.BasketApi.WebApi.Services;
using HasanFurkanFidan.BasketApi.WebApi.Settings;
using HasanFurkanFidan.UdemyCourse.SHARED.IdentityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            services.AddSingleton<RedisManager>(sp=> {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisManager(redisSettings.Host, redisSettings.Port,redisSettings.Password);
                redis.Connect();
                return redis;
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketService, BasketManager>();
            services.AddScoped<IIdentityService, IdentityManager>();
            services.AddControllers(opt=>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Configuration["IdentityServerUrl"];
                options.Audience = "resource_basket";

            });
            services.AddAuthentication();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HasanFurkanFidan.BasketApi.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HasanFurkanFidan.BasketApi.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
