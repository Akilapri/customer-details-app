using Castle.Core.Resource;
using CustomerDetailsWebApi;
using CustomerDetailsWebApi.Controllers;
using CustomerDetailsWebApi.Model;
using CustomerDetailsWebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CustomerDetailsTests
{
    public class CustomerDetailsControllerTests
    {
        private readonly Mock<ICustomerDetailsRepository> _repositoryMock;
        private readonly Mock<ILogger<CustomerDetailsController>> _loggerMock;
        private List<Customer> _customers;
        private readonly CustomerDetailsController _sut;

        public CustomerDetailsControllerTests()
        {
            // Shared setup for all tests
            _repositoryMock = new Mock<ICustomerDetailsRepository>();
            _loggerMock = new Mock<ILogger<CustomerDetailsController>>();
            _sut = new CustomerDetailsController(_repositoryMock.Object, _loggerMock.Object);

        }

        private static List<Customer> DataSetUp()
        {
            return new List<Customer>
        {
            new Customer { Id = 1, FirstName = "Customer1", LastName = "Name1" },
            new Customer { Id = 2, FirstName = "Customer2", LastName = "Name2" }
        };
        }


        [Fact]
        public async Task GetCustomers_ReturnsAllCustomers()
        {
            // Arrange
            var customers = DataSetUp();

            _repositoryMock.Setup(repo => repo.GetCustomersAsync()).ReturnsAsync(customers);

            // Act
            var result = await _sut.GetCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetCustomer_ReturnsCustomerById()
        {
            // Arrange

            var customers = DataSetUp();

            var customerToReturn = customers.FirstOrDefault(c => c.Id == 1);

            _repositoryMock.Setup(repo => repo.GetCustomerByIdAsync(1)).ReturnsAsync(customerToReturn);

            // Act
            var result = await _sut.GetCustomer(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(1, model.Id);
            Assert.Equal("Customer1", model.FirstName);
            Assert.Equal("Name1", model.LastName);
        }

        [Fact]
        public async Task PostCustomer_CreatesNewCustomer()
        {
            // Arrange
            var newCustomer = new Customer { FirstName = "NewCustomer", LastName = "Ted" };

            _repositoryMock.Setup(repo => repo.AddCustomerAsync(newCustomer));

            // Act
            var result = await _sut.PostCustomer(newCustomer);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            var model = Assert.IsType<Customer>(createdAtActionResult.Value);

            Assert.Equal("NewCustomer", model.FirstName);
            Assert.Equal("Ted", model.LastName);

            _repositoryMock.Verify(repo => repo.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task PutCustomer_UpdatesExistingCustomer()
        {
            // Arrange
            var customers = DataSetUp();

            var customerToUpdate = customers.FirstOrDefault(c => c.Id == 1);

            _repositoryMock.Setup(repo => repo.UpdateCustomerAsync(customerToUpdate));

            var updatedCustomer = new Customer { Id = 1, FirstName = "UpdatedCustomer", LastName = "UpdatedName" };

            // Act
            var result = await _sut.PutCustomer(1, updatedCustomer);

            // Assert
            Assert.IsType<NoContentResult>(result);
           
            _repositoryMock.Verify(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Once);

        }

        [Fact]
        public async Task DeleteCustomer_DeletesExistingCustomer()
        {
            // Arrange
            var customers = DataSetUp();

            var customerToDelete = customers.FirstOrDefault(c => c.Id == 1);

            _repositoryMock.Setup(repo => repo.DeleteCustomerAsync(customerToDelete.Id));

            // Act
            var result = await _sut.DeleteCustomer(1);

            var deleted = _sut.GetCustomer(1).Result.Value;

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(deleted);

            _repositoryMock.Verify(repo => repo.DeleteCustomerAsync(It.IsAny<int>()), Times.Once);
        }
    }

}