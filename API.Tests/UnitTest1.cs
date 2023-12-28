using StepMaster.Services.ForDb.Interfaces;

namespace API.Tests
{
    public class UnitTest1
    {
        private readonly StepMaster.Services.ForDb.Interfaces.IRegion_Service _regions;
        public RegionsController(IRegion_Service regions)
        {
            _regions = regions;
        }
        [Fact]
        public void Test1()
        {

        }
    }
}