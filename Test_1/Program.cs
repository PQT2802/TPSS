
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TPSS.API.Helper;
using TPSS.Business.Exceptions;
using TPSS.Data.Models.Entities;

namespace Test_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            } ).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
                { 
                    //tu cap token
                  ValidateIssuer = true,
                  ValidateAudience = true,

                  //ky vao token
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nX9IwCQbu6IEQWVFZijgk8miXIZtZ9PGGQyamYGcyl2Oq1xr5wUgDYBmfkuUPxeMIBE1CnRCE3yZIdFXWgJo4V1frk4dFGup6Nyy"))
                }; 
            });

            builder.Services.AddServicesConfiguration();
            builder.Services.AddProblemDetails();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");  
            app.UseHttpsRedirection();

            
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
