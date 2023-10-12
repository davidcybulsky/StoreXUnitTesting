using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Assert = Xunit.Assert;

namespace TestProject
{
    public class OrderCrudUnitTests
    {
        [Fact]
        public void Test1()
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
            Assert.NotNull(createdOrder);
            Assert.Equal(order, createdOrder);
        }

        [Fact]
        public void Test2()
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
            Assert.NotNull(updatedOrder);
            Assert.Equal(order, updatedOrder);
            Assert.Equal(order.CustomerId, updatedOrder.CustomerId);
            Assert.Equal(order.Status, updatedOrder.Status);
            Assert.Equal(order.Products, updatedOrder.Products);
        }

        [Fact]
        public void Test3()
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
            Order createdOrder = orderCRUD.Read(order.Id);
            orderCRUD.Delete(order.Id);
            Order deletedOreder = orderCRUD.Read(order.Id);

            //assert 
            Assert.Equal(order, createdOrder);
            Assert.Null(deletedOreder);
        }

        [Fact]
        public void Test4()
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
        }


        [Fact]
        public void Test5()
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
        }
    }
}
