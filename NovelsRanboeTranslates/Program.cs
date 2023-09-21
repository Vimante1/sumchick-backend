using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Repository.Repositories;
using NovelsRanboeTranslates.Services.Interfraces;
using NovelsRanboeTranslates.Services.Services;
using System.Net.Http.Headers;
using System.Text;

namespace NovelsRanboeTranslates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IMongoDbSettings>(sp =>
            {
                var conf = builder.Configuration.GetSection("MongoDbConnection");
                return new MongoDbSettings
                {
                    ConnectionString = conf.GetValue<string>("ConnectionString"),
                    DatabaseName = conf.GetValue<string>("DatabaseName")
                };
            });



            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
            builder.Services.Configure<PaypalCredentials>(builder.Configuration.GetSection("PaypalCredentials"));
            builder.Services.Configure<AdvCashPassword>(builder.Configuration.GetSection("AdvCashPassword"));
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });
            builder.Services.AddSingleton<JWTSettings>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

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
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IChapterService, ChapterService>();
            builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
            builder.Services.AddScoped<ICommentsService, CommentsService>();
            builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseCors("AllowSpecificOrigins");
            app.Use(async (context, next) =>
            {
                string token = context.Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token).ToString();
                }
                await next.Invoke();
            });

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