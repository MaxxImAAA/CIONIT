
using labaOnit.Data;
using labaOnit.InterfacesAndRealization;
using labaOnit.Models;
using Microsoft.EntityFrameworkCore;

namespace labaOnit
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql("Host=localhost;Port=5432;Database=labaonitdb;Username=postgres;Password=12345");
            });

            builder.Services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
          
            app.Run();

        }
    }
}
