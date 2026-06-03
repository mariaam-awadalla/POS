
using Microsoft.EntityFrameworkCore;
using POS.API.Data;
using POS.API.Services;


namespace POS.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //for db context and sql
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserService, UserService>();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            //auto update db 
            await SeedDataAsync(app);

            app.Run();


        }
        static async Task SeedDataAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();
        }
    }
}
