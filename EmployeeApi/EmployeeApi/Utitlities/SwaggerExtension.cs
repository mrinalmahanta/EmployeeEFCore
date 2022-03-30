using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EmployeeApi.Utitlities
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.IncludeXmlComments(Swagger.SwaggerXmlCommentsPath);
                    c.SwaggerDoc(Swagger.Name, new OpenApiInfo
                    {
                        Version = "v1",
                        Title ="Emplyee API",
                        Description = "Employee CRUD"
                    });

                    c.EnableAnnotations();
                });

            services.AddSwaggerGenNewtonsoftSupport();
        }
        public static void UseSwaggerConfig(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Swagger.Endpoint, Swagger.Description);
                c.DisplayRequestDuration();
            });
        }
    }
}
