
using InventoryManagementAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

namespace InventoryManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // -----------------------------
            // BUILD
            // -----------------------------
            var builder = WebApplication.CreateBuilder(args);

            // -----------------------------
            // SERVICES
            // -----------------------------

            // Add controllers
            builder.Services.AddControllers();

            // Add DbContext (PostgreSQL)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Inventory API",
                    Version = "v1"
                });
            });

            // Add CORS (Flutter needs this)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // -----------------------------
            // APP
            // -----------------------------
            var app = builder.Build();

            // -----------------------------
            // MIDDLEWARE
            // -----------------------------

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            // -----------------------------
            app.Run();
        }
    }
}
