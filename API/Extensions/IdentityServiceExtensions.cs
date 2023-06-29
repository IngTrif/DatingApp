

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public  static class IdentityServiceExtensions
    {
       public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config) 
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding
                    .UTF8.GetBytes(config["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false
      //this gives to our server enought info to take a look at the token and validate just based on the issuers signing key which wehave inplamentet
    };

  });

        return services;
        }
    }
}