
using Dynatron.Application.Interfaces;
using Dynatron.Infrastructure.Configurations;
using Dynatron.Infrastructure.Database;
using Dynatron.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dynatron.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var appSettings = new AppSettings(builder.Configuration);
            builder.Services.AddSingleton<IAppSettings, AppSettings>();


            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins($"{appSettings.CorsOrigins}")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // InMemory SQL Connection (only for demonstration)
            // Use SQLServer in development
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDynatronDB");
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IRepository, Repository>();

            // Configure swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                // Recreate database
                var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                // Seed sample data
                new DataSeeder(dbContext).SeedAll();
            }

            app.Run();
        }
    }
}
