using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Xunit.Abstractions;

namespace TestProject
{
    public class CustomerCrudUnitTests
    {
        private readonly ITestOutputHelper _output;

        public CustomerCrudUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CustomerCreation_ForCorrectCustomer_CreatesGivenCustomerInDb()
        {
            //arrange
            ICRUD<Customer> customerCRUD = new CustomerService(new StoreContext());
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@email.com"
            };

            //act
            customerCRUD.Create(customer);
            var customer2 = customerCRUD.Read(customer.Id);

            //assert
            Assert.Equal(customer, customer2);
            _output.WriteLine("Customer creation works");
        }

        [Fact]
        public void CustomerUpdate_ForCorrectCustomerToUpdate_UpdatesCustomerWhoIsInDb()
        {

            //arrange
            ICRUD<Customer> customerCRUD = new CustomerService(new StoreContext());
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@email.com"
            };
            Customer customer2 = new()
            {
                FirstName = "Pawe³",
                LastName = "Nowak",
                Email = "pawe³.nowak@email.com"
            };

            //act
            customerCRUD.Create(customer);
            customerCRUD.Update(customer.Id, customer2);
            Customer updatedUser = customerCRUD.Read(customer.Id);

            //assert
            Assert.Equal(customer, updatedUser);
            _output.WriteLine("Customer update works");
        }

        [Fact]
        public void CustomerDeletion_ForCorrectId_DeletesCustomerWithGivenId()
        {
            //arrange
            ICRUD<Customer> customerCRUD = new CustomerService(new StoreContext());
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = " Kowalski",
                Email = "jan.lowalski@Email"
            };

            //act
            customerCRUD.Create(customer);
            customerCRUD.Delete(customer.Id);
            Customer customerDeleted = customerCRUD.Read(customer.Id);

            //assert
            Assert.Null(customerDeleted);
            _output.WriteLine("Customer deletion works");
        }
    }
}