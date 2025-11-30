using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Data;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dsw2025Tpi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddDbContext<Dsw2025TpiContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("Dsw2025TpiDb"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()
            ));



        builder.Services.AddScoped<IRepository, EfRepository>();
        builder.Services.AddTransient<ProductsManagementService>();
        builder.Services.AddTransient<CustomerManagementService>();
        builder.Services.AddTransient<OrderManagementService>();
        builder.Services.AddTransient<AuthManagementService>();

        // 👉 Autenticación JWT
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHealthChecks("/healthcheck");

        app.Run();
    }
}
