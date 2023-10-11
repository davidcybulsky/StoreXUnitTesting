using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;

namespace TestProject
{
    public class ProductCrudUnitTests
    {
        [Fact]
        public void Test1()
        {
            //arrange
            ICRUD<Product> productCRUD = new ProductCRUD(new StoreContext());
            Product product = new()
            {
                Name = "Krajzega",
                Price = 499.99,
                Availability = Availability.AVAILABILITY.AVAILABLE
            };

            //act 
            productCRUD.Create(product);
            Product createdProduct = productCRUD.Read(product.Id);

            //assert
            Assert.NotNull(createdProduct);
            Assert.Equal(product, createdProduct);
        }

        [Fact]
        public void Test2()
        {
            //arrange
            ICRUD<Product> productCRUD = new ProductCRUD(new StoreContext());
            Product product = new()
            {
                Name = "Krajzega",
                Price = 499.99,
                Availability = Availability.AVAILABILITY.AVAILABLE
            };
            Product productToUpdate = new()
            {
                Name = product.Name,
                Price = 399.99,
                Availability = Availability.AVAILABILITY.UNAVAILABLE
            };

            //act 
            productCRUD.Create(product);
            productCRUD.Update(product.Id, productToUpdate);
            Product updatedProduct = productCRUD.Read(product.Id);

            //assert
            Assert.NotNull(updatedProduct);
            Assert.Equal(product, updatedProduct);
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Price, updatedProduct.Price);
            Assert.Equal(product.Availability, updatedProduct.Availability);
        }

        [Fact]
        public void Test3()
        {
            //arrange
            ICRUD<Product> productCRUD = new ProductCRUD(new StoreContext());
            Product product = new()
            {
                Name = "Krajzega",
                Price = 499.99,
                Availability = Availability.AVAILABILITY.AVAILABLE
            };

            //act 
            productCRUD.Create(product);
            Product createdProduct = productCRUD.Read(product.Id);
            productCRUD.Delete(product.Id);
            Product deletedProduct = productCRUD.Read(product.Id);


            //assert
            Assert.NotNull(createdProduct);
            Assert.Null(deletedProduct);

        }
    }
}
