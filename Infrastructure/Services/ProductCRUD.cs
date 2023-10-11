using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class ProductCRUD : ICRUD<Product>
    {
        private readonly StoreContext _db;

        public ProductCRUD(StoreContext db)
        {
            _db = db;
        }

        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
