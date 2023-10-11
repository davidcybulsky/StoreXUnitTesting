using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;

namespace TestProject
{
    public class OrderCrudUnitTests
    {
        [Fact]
        public void Test1()
        {
            //arrange
            ICRUD<Order> orderCRUD = new OrderCRUD(new StoreContext());
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

            //act
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
            ICRUD<Order> orderCRUD = new OrderCRUD(new StoreContext());
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

            Order orderToUpdate = new()
            {
                CustomerId = order.Id,
                Products = order.Products,
                Status = Status.STATUS.INPROGRESS
            };

            //act
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
            ICRUD<Order> orderCRUD = new OrderCRUD(new StoreContext());
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

            //act
            orderCRUD.Create(order);
            Order createdOrder = orderCRUD.Read(order.Id);
            orderCRUD.Delete(order.Id);
            Order deletedOreder = orderCRUD.Read(order.Id);

            //assert 
            Assert.Equal(order, createdOrder);
            Assert.Null(deletedOreder);
        }
    }
}
