using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TestProject
{
    public class OrderCrudUnitTests
    {
        private readonly ITestOutputHelper _output;

        public OrderCrudUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void OrderCreation_ForCorrectOrder_CreatesGivenOrderInDb()
        {
            //arrange
            StoreContext context = new StoreContext();
            ICRUD<Order> orderCRUD = new OrderService(context);
            ICRUD<Customer> customerCRUD = new CustomerService(context);
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = "Paweł",
                Email = "janpawel@gmail.pl",
            };
            Order order = new()
            {
                CustomerId = customer.Id,
                Products = new List<Product>()
                {
                    new()
                    {
                        Name = "Foo",
                        Price = 99.99,
                        Availability = Availability.AVAILABILITY.AVAILABLE
                    }
                },
                Status = Status.STATUS.NEW
            };

            //act
            customerCRUD.Create(customer);
            orderCRUD.Create(order);
            Order createdOrder = orderCRUD.Read(order.Id);

            //assert
            Assert.Equal(order, createdOrder);
            _output.WriteLine("Order creation works");
        }

        [Fact]
        public void OrderUpdate_ForCorrectOrderToUpdate_UpdatesOrderWhoIsInDb()
        {
            //arrange
            StoreContext context = new StoreContext();
            ICRUD<Order> orderCRUD = new OrderService(context);
            ICRUD<Customer> customerCRUD = new CustomerService(context);
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = "Paweł",
                Email = "janpawel@gmail.pl",
            };
            Order order = new()
            {
                CustomerId = customer.Id,
                Products = new List<Product>()
                {
                    new()
                    {
                        Name = "Foo",
                        Price = 99.99,
                        Availability = Availability.AVAILABILITY.AVAILABLE
                    }
                },
                Status = Status.STATUS.NEW
            };

            Order orderToUpdate = new()
            {
                CustomerId = order.CustomerId,
                Products = order.Products,
                Status = Status.STATUS.INPROGRESS
            };

            //act
            customerCRUD.Create(customer);
            orderCRUD.Create(order);
            orderCRUD.Update(order.Id, orderToUpdate);
            Order updatedOrder = orderCRUD.Read(order.Id);

            //assert
            Assert.Equal(order, updatedOrder);
            _output.WriteLine("Order update works");
        }

        [Fact]
        public void OrderDeletion_ForCorrectId_DeletesOrderWithGivenId()
        {
            //arrange
            StoreContext context = new StoreContext();
            ICRUD<Order> orderCRUD = new OrderService(context);
            ICRUD<Customer> customerCRUD = new CustomerService(context);
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = "Paweł",
                Email = "janpawel@gmail.pl",
            };
            Order order = new()
            {
                CustomerId = customer.Id,
                Products = new List<Product>()
                {
                    new()
                    {
                        Name = "Foo",
                        Price = 99.99,
                        Availability = Availability.AVAILABILITY.AVAILABLE
                    }
                },
                Status = Status.STATUS.NEW
            };

            //act
            customerCRUD.Create(customer);
            orderCRUD.Create(order);
            orderCRUD.Delete(order.Id);
            Order deletedOreder = orderCRUD.Read(order.Id);

            //assert 
            Assert.Null(deletedOreder);
            _output.WriteLine("Order deletion works");
        }

        [Fact]
        public void BussinesLogicException_ForBadCustomerId_ThrowsException()
        {
            //arrange
            StoreContext context = new StoreContext();
            ICRUD<Order> orderCRUD = new OrderService(context);
            Order order = new()
            {
                CustomerId = Guid.NewGuid(),
                Products = new List<Product>()
                {
                    new()
                    {
                        Name = "Foo",
                        Price = 99.99,
                        Availability = Availability.AVAILABILITY.AVAILABLE
                    }
                },
                Status = Status.STATUS.NEW
            };

            //assert
            Assert.Throws<BusinessLogicException>(() => orderCRUD.Create(order));
            _output.WriteLine("Order expection works");
        }


        [Fact]
        public void BussinesLogicException_ForUnavailableProduct_ThrowsException()
        {
            //arrange
            StoreContext context = new StoreContext();
            ICRUD<Customer> customerCRUD = new CustomerService(context);
            ICRUD<Order> orderCRUD = new OrderService(context);
            Customer customer = new()
            {
                FirstName = "Jan",
                LastName = "Paweł",
                Email = "janpaul@gmail.ru"
            };

            Order order = new()
            {
                CustomerId = customer.Id,
                Products = new List<Product>()
                {
                    new()
                    {
                        Name = "Foo",
                        Price = 99.99,
                        Availability = Availability.AVAILABILITY.UNAVAILABLE
                    }
                },
                Status = Status.STATUS.NEW
            };

            //act
            customerCRUD.Create(customer);

            //assert
            Assert.Throws<BusinessLogicException>(() => orderCRUD.Create(order));
            _output.WriteLine("Order expection works");
        }
    }
}
