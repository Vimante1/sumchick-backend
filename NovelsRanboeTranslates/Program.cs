using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Repository.Repositories;
using NovelsRanboeTranslates.Services.Interfraces;
using NovelsRanboeTranslates.Services.Services;
using System.Text;

namespace NovelsRanboeTranslates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy =>
                {
                    policy.RequireRole("admin");
                });
            });
            builder.Services.AddSingleton<JWTSettings>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration.GetSection("JWTSettings:Audience").Value,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSettings:SecretKey").Value)),
                        ValidateIssuerSigningKey = true
                    };
                });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}