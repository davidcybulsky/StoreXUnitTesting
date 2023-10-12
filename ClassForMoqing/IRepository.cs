using ProjectForMoqing;

namespace ClassForMoqing
{
    public interface IRepository
    {
        Model GetModelById(int id);
    }
}