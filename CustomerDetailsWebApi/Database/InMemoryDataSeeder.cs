using CustomerDetailsWebApi.Model;
// InMemoryDataSeeder.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerDetailsWebApi.Database
{
    public class InMemoryDataSeeder : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                using (var scope = builder.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<CustomerDetailsDbContext>();
                    context.Database.EnsureCreated(); // Ensure the in-memory database is created

                    // Seed data
                    if (!context.Customers.Any())
                    {
                        context.Customers.Add(new Customer { FirstName = "John", LastName = "Doe" });
                        context.Customers.Add(new Customer { FirstName = "Jane", LastName = "Smith" });
                        context.SaveChanges();
                    }
                }

                next(builder);
            };
        }
    }
}
