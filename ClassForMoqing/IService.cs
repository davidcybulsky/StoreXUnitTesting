namespace ProjectForMoqing
{
    public interface IService
    {
        Model GetModel(int id);
        void Create(Model model);
    }
}
