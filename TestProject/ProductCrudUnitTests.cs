using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Xunit.Abstractions;

namespace TestProject
{
    public class ProductCrudUnitTests
    {
        private readonly ITestOutputHelper _output;

        public ProductCrudUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ProductCreation_ForCorrectProduct_CreatesGivenProductInDb()
        {
            //arrange
            ICRUD<Product> productCRUD = new ProductService(new StoreContext());
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
            Assert.Equal(product, createdProduct);
            _output.WriteLine("Product creation works");
        }

        [Fact]
        public void ProductUpdate_ForCorrectProductToUpdate_UpdatesProductWhoIsInDb()
        {
            //arrange
            ICRUD<Product> productCRUD = new ProductService(new StoreContext());
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
            Assert.Equal(product, updatedProduct);
            _output.WriteLine("Product update works");
        }

        [Fact]
        public void ProductDeletion_ForCorrectId_DeletesProductWithGivenId()
        {
            //arrange
            ICRUD<Product> productCRUD = new ProductService(new StoreContext());
            Product product = new()
            {
                Name = "Krajzega",
                Price = 499.99,
                Availability = Availability.AVAILABILITY.AVAILABLE
            };

            //act 
            productCRUD.Create(product);
            productCRUD.Delete(product.Id);
            Product deletedProduct = productCRUD.Read(product.Id);


            //assert
            Assert.Null(deletedProduct);
            _output.WriteLine("Product deletion works");

        }
    }
}
