

using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions // make the class static

    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration config)  

            {
                services.AddDbContext<DataContext>(opt => 
            {
                 opt.UseSqlite(config.GetConnectionString("DefaultConnection")); //options we pass trough to our expression->inside option different methots get conection string
            });
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>(); 
            services.AddScoped<IUserRepository, UserRepository>();//make sinjectable into user controller
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //assembly for the current domain
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();

            return services;
            }      
    }
}