using API.Errors;

using Core.Interfaces;

using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using StackExchange.Redis;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration          configuration)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<StoreContext>(
            opt =>
                {
                    opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                });

        services.AddSingleton<IConnectionMultiplexer>(
            c =>
                {
                    var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));

                    return ConnectionMultiplexer.Connect(options);
                });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddScoped(
            typeof(IGenericRepository<>),
            typeof(GenericRepository<>));

        services.AddScoped<ITokenService, TokenService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.Configure<ApiBehaviorOptions>(
            options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                        {
                            var errors = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToArray();

                            var errorResponse = new ApiValidationErrorResponse
                                {
                                    Errors = errors,
                                };

                            return new BadRequestObjectResult(errorResponse);
                        };
                });

        services.AddCors(
            opt =>
                {
                    opt.AddPolicy(
                        "CorsPolicy",
                        policy =>
                            {
                                policy.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .WithOrigins("https://localhost:4200");
                            });
                });

        return services;
    }
}
