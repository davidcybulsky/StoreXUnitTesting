using ClassForMoqing;
using NSubstitute;
using ProjectForMoqing;

namespace Moqing
{
    public class Moqing
    {
        private readonly IService _service;
        private readonly IRepository _repository = Substitute.For<IRepository>();
        public Moqing()
        {
            _service = new Service(_repository);
        }

        [Fact]
        public void Test1()
        {
            //arrange
            var modelId = 1;
            var modelName = "Test";
            var modelDescription = "Description";

            Model model = new()
            {
                Id = modelId,
                Name = modelName,
                Description = modelDescription
            };

            _repository.GetModelById(modelId).Returns(model);

            //act
            var modelInDb = _service.GetModel(modelId);

            //assert
            Assert.Equal(modelId, modelInDb.Id);
            Assert.Equal(modelName, modelInDb.Name);
            Assert.Equal(modelDescription, modelInDb.Description);
        }
    }
}