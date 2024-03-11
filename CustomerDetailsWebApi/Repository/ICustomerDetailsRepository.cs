using CustomerDetailsWebApi.Model;

namespace CustomerDetailsWebApi.Repository
{
    public interface ICustomerDetailsRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }

}
