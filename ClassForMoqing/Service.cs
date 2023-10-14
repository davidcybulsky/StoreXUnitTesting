using ClassForMoqing;

namespace ProjectForMoqing
{
    public class Service : IService
    {
        private readonly IRepository _repository;

        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public void Create(Model model)
        {
            _repository.CreateModel(model);
        }

        public Model GetModel(int id)
        {
            var model = _repository.GetModelById(id);
            return model;
        }
    }
}
