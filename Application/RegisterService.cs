using Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class RegisterService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            
            return services;
        }
    }
}
