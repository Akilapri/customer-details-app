using CustomerDetailsWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetailsWebApi
{

    public class CustomerDetailsDbContext : DbContext
    {
        public CustomerDetailsDbContext(DbContextOptions<CustomerDetailsDbContext> options)
            : base(options)
        {
        }

        public CustomerDetailsDbContext()
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}
