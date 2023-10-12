using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Exceptions;

namespace Infrastructure.Services
{
    public class OrderService : ICRUD<Order>
    {
        private readonly StoreContext _db;

        public OrderService(StoreContext db)
        {
            _db = db;
        }

        public void Create(Order entity)
        {
            var query =
                from c in _db.Customers
                where c.Id == entity.CustomerId
                select c;

            var customer = query.FirstOrDefault();

            if (customer is null)
            {
                throw new BusinessLogicException();
            }

            foreach (var product in entity.Products)
            {
                if (product.Availability == Domain.Constants.Availability.AVAILABILITY.UNAVAILABLE)
                {
                    throw new BusinessLogicException();
                }
            }

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
