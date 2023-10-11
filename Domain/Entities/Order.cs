using Domain.Constants;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public required Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public List<Product> Products { get; set; } = new();
        public Status.STATUS Status { get; set; } = Constants.Status.STATUS.NEW;
    }
}
