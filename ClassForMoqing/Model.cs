namespace ProjectForMoqing
{
    public class Model
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Model)
            {
                if (((Model)obj).Id != Id)
                {
                    return false;
                }
                if (((Model)obj).Name != Name)
                {
                    return false;
                }
                if (((Model)obj).Description != Description)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() *
                Name.GetHashCode() *
                Description.GetHashCode();
        }
    }
}
