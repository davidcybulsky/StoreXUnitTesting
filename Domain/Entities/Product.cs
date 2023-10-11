using Domain.Constants;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Availability.AVAILABILITY Availability { get; set; } = Constants.Availability.AVAILABILITY.UNAVAILABLE;
    }
}
