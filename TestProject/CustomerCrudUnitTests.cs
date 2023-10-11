using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;

namespace TestProject
{
    public class CustomerCrudUnitTests
    {
        [Fact]
        public void Test1()
        {
            //arrange
            ICRUD<Customer> customerCRUD = new CustomerCRUD(new StoreContext());
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
            Assert.NotNull(customer2);
            Assert.Equal(customer, customer2);
        }

        [Fact]
        public void Test2()
        {

            //arrange
            ICRUD<Customer> customerCRUD = new CustomerCRUD(new StoreContext());
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
            Assert.NotNull(updatedUser);
            Assert.Equal(customer, updatedUser);
            Assert.Equal(customer.FirstName, updatedUser.FirstName);
            Assert.Equal(customer.LastName, updatedUser.LastName);
            Assert.Equal(customer.Email, updatedUser.Email);
        }

        [Fact]
        public void Test3()
        {
            //arrange
            ICRUD<Customer> customerCRUD = new CustomerCRUD(new StoreContext());
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = " Kowalski",
                Email = "jan.lowalski@Email"
            };

            //act
            customerCRUD.Create(customer);
            Customer customerCreated = customerCRUD.Read(customer.Id);
            customerCRUD.Delete(customer.Id);
            Customer customerDeleted = customerCRUD.Read(customer.Id);

            //assert
            Assert.Equal(customer, customerCreated);
            Assert.Null(customerDeleted);
        }
    }
}