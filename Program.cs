using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Core.Services;
using OrchidPharmedApi.DataAccess.DataContext;
using OrchidPharmedApi.DataAccess.Repositories;
using OrchidPharmedApi.Validators;
using System.Text;

public class Program  // Ensure Program class is public
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Add the DbContext with SQL Server (or PostgreSQL if using it).
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register repositories and services for dependency injection.
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<ITaskEntityRepository, TaskEntityRepository>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<ITaskEntityService, TaskEntityService>();

        // Add FluentValidation for request validation.
        builder.Services.AddValidatorsFromAssemblyContaining<ProjectValidator>();

        // Add Swagger for API documentation.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure JWT Authentication.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero  // Ensures tokens expire exactly at token expiration time
                };
            });

        var app = builder.Build();

        // Apply pending migrations automatically when the app starts (optional).
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate(); // Apply pending migrations, if any
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Enable global CORS policy (optional).
        // Uncomment this block if you need CORS support for cross-origin requests.
        /*
        app.UseCors(policy =>
            policy.WithOrigins("https://allowed-origin.com") // Replace with your front-end URL
                  .AllowAnyMethod()
                  .AllowAnyHeader());
        */

        // Global error handling middleware (optional).
        // This ensures all unhandled exceptions are caught and handled in a standardized way.
        // app.UseMiddleware<GlobalErrorHandlingMiddleware>();

        // Enable authentication and authorization middleware.
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
