using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class OrderCRUD : ICRUD<Order>
    {
        private readonly StoreContext _db;

        public OrderCRUD(StoreContext db)
        {
            _db = db;
        }

        public void Create(Order entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var query =
                from order in _db.Orders
                where order.Id == id
                select order;
            var result = query.FirstOrDefault();
            _db.Remove(result);
            _db.SaveChanges();
        }

        public Order Read(Guid id)
        {
            var query =
                from order in _db.Orders
                where order.Id == id
                select order;
            var result = query.FirstOrDefault();
            return result;
        }

        public void Update(Guid id, Order entity)
        {
            var query =
                from order in _db.Orders
                where order.Id == id
                select order;
            var result = query.FirstOrDefault();
            if (result is not null)
            {
                result.Products = entity.Products;
                result.Status = result.Status;
                _db.SaveChanges();
            }
        }
    }
}
