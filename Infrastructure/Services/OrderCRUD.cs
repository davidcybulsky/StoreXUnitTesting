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
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Order Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
