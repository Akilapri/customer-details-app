using CustomerDetailsWebApi.Database;
using CustomerDetailsWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetailsWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<CustomerDetailsDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));

            // Seed in-memory data
            services.AddTransient<IStartupFilter, InMemoryDataSeeder>();

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<ICustomerDetailsRepository, CustomerDetailsRepository>();

            services.AddCors();
        }

        private static void Configure(WebApplication app)
        {
            var env = app.Environment;

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:3002")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
