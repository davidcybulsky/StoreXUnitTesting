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
            _db.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var query =
                from product in _db.Products
                where product.Id == id
                select product;
            var result = query.FirstOrDefault();
            _db.Remove(result);
            _db.SaveChanges();
        }

        public Product Read(Guid id)
        {
            var query =
                from product in _db.Products
                where product.Id == id
                select product;
            var result = query.FirstOrDefault();
            return result;
        }

        public void Update(Guid id, Product entity)
        {
            var query =
                from product in _db.Products
                where product.Id == id
                select product;
            var result = query.FirstOrDefault();
            if (result is not null)
            {
                result.Name = entity.Name;
                result.Price = entity.Price;
                result.Availability = entity.Availability;
                _db.SaveChanges();
            }
        }
    }
}
