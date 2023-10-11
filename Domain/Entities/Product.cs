using Domain.Constants;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required double Price { get; set; }
        public required Availability.AVAILABILITY Availability { get; set; }
    }
}
