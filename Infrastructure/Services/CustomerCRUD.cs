using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class CustomerCRUD : ICRUD<Customer>
    {
        private readonly StoreContext _db;

        public CustomerCRUD(StoreContext db)
        {
            _db = db;
        }

        public void Create(Customer entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var query =
                from customer in _db.Customers
                where customer.Id == id
                select customer;
            var result = query.FirstOrDefault();
            _db.Remove(result);
            _db.SaveChanges();
        }

        public Customer Read(Guid id)
        {
            var query =
                from customer in _db.Customers
                where customer.Id == id
                select customer;
            var result = query.FirstOrDefault();
            return result;
        }

        public void Update(Guid id, Customer entity)
        {
            var query =
                from customer in _db.Customers
                where customer.Id == id
                select customer;
            var result = query.FirstOrDefault();
            if (result is not null)
            {
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.Email = entity.Email;
                _db.SaveChanges();
            }
        }
    }
}
