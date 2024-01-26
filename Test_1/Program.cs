
using TPSS.API.Helper;
using TPSS.Business.Exceptions;

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
            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
