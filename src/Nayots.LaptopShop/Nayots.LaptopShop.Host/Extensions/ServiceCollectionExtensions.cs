using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nayots.LaptopShop.BL.Services.Auth;
using Nayots.LaptopShop.BL.Services.Cart;
using Nayots.LaptopShop.BL.Services.Products;
using Nayots.LaptopShop.BL.Services.Users;
using Nayots.LaptopShop.BL.Validation;
using Nayots.LaptopShop.Common.Contracts.Auth;
using Nayots.LaptopShop.Common.Contracts.Cart;
using Nayots.LaptopShop.Common.Contracts.Data;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Contracts.Users;
using Nayots.LaptopShop.Common.Models.Config;
using Nayots.LaptopShop.Data.BootStrap;
using Nayots.LaptopShop.Data.Cart;
using Nayots.LaptopShop.Data.Products;
using Nayots.LaptopShop.Data.Shared;
using System;
using System.Text;

namespace Nayots.LaptopShop.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nayots.LaptopShop.Host", Version = "v1" });
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
        }

        public static IServiceCollection AddJWTAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("Auth:Secret");
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new InvalidOperationException("Cannot have an empty secret");

            serviceCollection.AddSingleton<IUsersService, UsersService>();
            serviceCollection.AddSingleton<IJwtAuthManager>(x => new JwtAuthManager(secretKey, x.GetRequiredService<IUsersService>()));
            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return serviceCollection;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IProductsService, ProductsService>()
                .AddSingleton<IProductsRepository, ProductsRepository>()
                .AddSingleton<ICartRespository, CartRespository>()
                .AddSingleton<ICartService, CartService>();
        }

        public static IServiceCollection AddDB(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<DbConfig>(configuration.GetSection(nameof(DbConfig)));
            serviceCollection.AddSingleton<IConnectionProvider, SqliteConnectionProvider>();
            serviceCollection.AddSingleton<IDataBoostrap, DataBootstrap>();

            return serviceCollection;
        }

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CartAdditionValidator>());

            return serviceCollection;
        }
    }
}
