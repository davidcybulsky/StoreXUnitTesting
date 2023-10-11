using Domain.Constants;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public required Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public required List<Product> Products { get; set; }
        public required Status.STATUS Status { get; set; }
    }
}
