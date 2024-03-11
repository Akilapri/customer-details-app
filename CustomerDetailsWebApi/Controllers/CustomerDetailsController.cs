using CustomerDetailsWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using CustomerDetailsWebApi.Repository;

namespace CustomerDetailsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ICustomerDetailsRepository _repo;
        private readonly ILogger<CustomerDetailsController> _logger;

        public CustomerDetailsController(ICustomerDetailsRepository repo, ILogger<CustomerDetailsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/CustomerDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _repo.GetCustomersAsync();
            return Ok(customers);
        }

        // GET: api/CustomerDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _repo.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/CustomerDetails
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                await _repo.AddCustomerAsync(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PostCustomer: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest("Invalid customer ID");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _repo.UpdateCustomerAsync(customer);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutCustomer: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _repo.DeleteCustomerAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCustomer: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
