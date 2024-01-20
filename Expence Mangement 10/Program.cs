using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Expence_Mangement_10.Services;

namespace Expence_Mangement_10
{
    public class Startup
    {
        // Other configurations...
        

        public void ConfigureServices(IServiceCollection services)
        {
            



            //services.AddHttpsRedirection(options =>
           // {
            //options.HttpsPort = 443; // Default HTTPS port
           // });

            services.AddScoped<ExpenseCategoryService>();
            services.AddScoped<ExpenseService>();
            services.AddScoped<UserService>();


            // Configure JWT authentication
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options  =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, // Enable token lifetime validation
                    ClockSkew = TimeSpan.Zero // Set to zero to account for token expiration
                };
            });

            

            // Add Swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Expense Management API", Version = "v1" });
            });

            // Other configurations...
            
        }

        // Other methods...

        //it should be IHostingEnvironment here H
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            // Other configurations...

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger-UI (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense Management API V1");
                c.DocumentTitle = "Expense Management API";
                c.DocExpansion(DocExpansion.None);
            });

            // Other configurations...
        }

        // Other methods...
    }
}