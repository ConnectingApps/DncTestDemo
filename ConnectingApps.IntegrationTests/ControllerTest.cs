using ConnectingApps.TestDemo;
using ConnectingApps.TestDemo.Controllers;
using ConnectingApps.TestEnablers;
using Xunit;

namespace ConnectingApps.IntegrationTests
{
    public class ControllerTest : IntegrationTestBase<Startup,WeatherForecastController>
    {
        private WeatherForecastController _testInstance;

        public ControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void VerifyIfInstanceExist()
        {
            Assert.NotNull(_testInstance);
        }

        protected override void SetTestInstance(WeatherForecastController testInstance)
        {
            _testInstance = testInstance;
        }
    }
}
