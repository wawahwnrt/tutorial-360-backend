using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Application.Services;
using tutorial_backend_dotnet.Infrastructure.Data;
using tutorial_backend_dotnet.Infrastructure.Repositories;
using tutorial_backend_dotnet.Presentation.Filters;

namespace tutorial_backend_dotnet
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext with PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                //    "DefaultConnection": "Host=183.90.170.102;database=siteview;Username=postgres;password=Sr5BVN@4csr6Tg^2!7fj;Port=15432"
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions =>
                    {
                        // Optional: Enable advanced PostgreSQL features
                        npgsqlOptions.EnableRetryOnFailure(5); // Retry up to 5 times
                    }));

            // Add AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add Repositories
            services.AddScoped<ITutorialGroupRepository, TutorialGroupRepository>();
            services.AddScoped<ITutorialStepRepository, TutorialStepRepository>();
            services.AddScoped<IUserTutorialProgressRepository, UserTutorialProgressRepository>();

            // Add Unit of Work (if implemented)
            // services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Services
            services.AddScoped<ITutorialGroupService, TutorialGroupService>();
            services.AddScoped<ITutorialStepService, TutorialStepService>();
            services.AddScoped<IUserTutorialService, UserTutorialService>();
            
            // Add CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Angular dev server
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Allow cookies if needed
                });
            });

            // Add Controllers
            services.AddControllers();

            // Configure Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tutorial API",
                    Version = "v1",
                    Description = "API for managing tutorial groups and steps"
                });

                // Register parameter filters
                options.ParameterFilter<DefaultGroupIdParameterFilter>();
                options.ParameterFilter<DefaultRoleIdParameterFilter>();
                options.ParameterFilter<DefaultUserIdParameterFilter>();
            });

            // Add other services as needed
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutorial API v1");
            });
            app.UseCors("AllowAngularApp");

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
