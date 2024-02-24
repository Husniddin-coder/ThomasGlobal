using Application.Abstraction;
using Application.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class RegisterService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAppDbContext, AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IAcceptRepository, AcceptRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();

            return services;
        }
    }
}
