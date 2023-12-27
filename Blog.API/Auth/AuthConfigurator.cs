using Blog.App.Db.Entities;
using Blog.Domain.Entities;
using Blog.Infrastructure.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Blog.API.Auth
{
    public static class AuthConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var issuer = builder.Configuration["Jwt:Issuer"]!;
            var audience = builder.Configuration["Jwt:Audience"]!;
            var key = builder.Configuration["Jwt:Key"]!;

            builder.Services.Configure<JwtSettings>(s =>
            {
                s.Issuer = issuer;
                s.Audience = audience;
                s.SecretKey = key;
            });

            builder.Services.AddTransient<TokenGenerator>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser",
                    policy => policy.RequireClaim(ClaimTypes.Role, "user"));

                options.AddPolicy("Admin",
                    policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
            });

            builder.Services
                .AddIdentity<UserEntity, RoleEntity>(o =>
                {
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = true;
                    o.Password.RequireNonAlphanumeric = true;
                    o.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}