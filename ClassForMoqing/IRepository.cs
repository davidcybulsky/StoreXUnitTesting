using ProjectForMoqing;

namespace ClassForMoqing
{
    public interface IRepository
    {
        Model GetModelById(int id);
        void CreateModel(Model model);
    }
}