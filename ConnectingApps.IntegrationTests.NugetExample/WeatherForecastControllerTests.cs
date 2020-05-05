using System.Collections.Generic;
using System.Linq;
using ConnectingApps.TestDemo;
using ConnectingApps.TestDemo.Controllers;
using ConnectingApps.TestEnablers;
using Xunit;

namespace ConnectingApps.IntegrationTests.NugetExample
{
    public class WeatherForecastControllerTests :  IntegrationTestBase<Startup, WeatherForecastController>
    {
        private WeatherForecastController _weatherForecastController;

        public WeatherForecastControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void VerifyGetter()
        {
            var output = _weatherForecastController.Get();
            Assert.Equal(5, output.Count());
        }

        protected override void SetTestInstance(WeatherForecastController testInstance)
        {
            _weatherForecastController = testInstance;
        }

        protected override Dictionary<string, string> GetConfiguration()
        {
            var configuration = base.GetConfiguration();
            configuration.Add("AppSettingsKey","AppSettingsValue");
            return configuration;
        }
    }
}
