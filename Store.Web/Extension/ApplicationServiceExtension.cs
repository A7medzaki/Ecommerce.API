using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.ProductService;
using Store.Service.Services.ProductService.DTOs;
using Store.Service.Services.ProductService;
using Store.Service.HandleResponses;
using Store.Service.Services.CacheService;
using Store.Service.Services.BasketService.DTOs;
using Store.Service.Services.BasketService;
using Store.Repository.Basket;
using Store.Service.Services.TokenService;
using Store.Service.Services.UserService;
using Store.Service.Services.OrderService.DTOs;
using Store.Service.Services.OrderService;
using Store.Service.Services.PaymentService;

namespace Store.Web.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<IBasketRepository, BasketRepository>();
            
            services.AddScoped<IBasketService, BasketService>();
            
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IPaymentService, PaymentService >();

            services.AddAutoMapper(typeof(ProductProfile));

            services.AddAutoMapper(typeof(OrderProfile));

            services.AddAutoMapper(typeof(BasketProfile));

            services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState
                            .Where(model => model.Value?.Errors.Count > 0)
                            .SelectMany(model => model.Value?.Errors)
                            .Select(error => error.ErrorMessage)
                            .ToList();
                        var errorResponse = new ValidationErrorResponse
                        {
                            Errors = errors
                        };

                        return new BadRequestObjectResult(errorResponse);
                    };
                });

            return services;
        }
    }
}
