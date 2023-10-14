using ClassForMoqing;
using NSubstitute;
using ProjectForMoqing;
using Xunit.Abstractions;

namespace Moqing
{
    public class Moqing
    {
        private readonly ITestOutputHelper _output;

        public Moqing(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ModelRead_ForCorrectId_ShouldReturnModel()
        {
            //arrange
            IRepository _repository = Substitute.For<IRepository>();
            IService _service = new Service(_repository);
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
            Assert.Equal(model, modelInDb);
            _output.WriteLine("Model read works");
        }

        [Fact]
        public void ModelCreation_ForOneModel_ShouldExecuteOnce()
        {
            //arrange
            IRepository _repository = Substitute.For<IRepository>();
            IService _service = new Service(_repository); ;
            var counter = 0;
            var modelId = 1;
            var modelName = "Test";
            var modelDescription = "Description";

            Model model = new()
            {
                Id = modelId,
                Name = modelName,
                Description = modelDescription
            };

            //act 
            _repository.When(x => x.CreateModel(model)).Do(x => counter++);

            //assert
            Assert.Equal(0, counter);
            _output.WriteLine("Model creation works");
        }

        [Theory]
        [InlineData(1, "Title", "Description")]
        [InlineData(67, "P¹czek", "P¹czek z marmolad¹")]
        [InlineData(545, "Leksykon zwierz¹t", "Wielki leksykon zwierz¹t zawieraj¹cy ró¿ne gatunki z ca³ego œwiata")]
        [InlineData(6861, "Dummy title", "Here is a dummy description")]
        [InlineData(534535, "IPhone16", "New generation of IPhone")]
        public void InlineUserReadTest_ForCorrectId_ShouldReturnModel(int modelId, string modelName, string modelDescription)
        {
            //arrange
            IRepository _repository = Substitute.For<IRepository>();
            IService _service = new Service(_repository);

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
            Assert.Equal(model, modelInDb);
            _output.WriteLine("Model deletion works");
        }
    }
}