using System.Text;
using Core.Entities;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions.AppExtensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServicesAndRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<QuestRepository, QuestRepository>();

        services.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DatabaseContext>() //this creates the necessary tables to store the users
            .AddSignInManager<SignInManager<User>>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname",
                };
            });
        
        
        services.AddAuthorization();
            
        
        return services;
    }
}