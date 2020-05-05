using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConnectingApps.TestDemo;
using ConnectingApps.TestDemo.RefitClient;
using ConnectingApps.TestEnablers;
using Refit;
using Xunit;

namespace ConnectingApps.IntegrationTests.NugetExample
{
    public class WeatherForecastClientTest : RefitClientTestBase<Startup, IWeatherForecastClient>
    {
        public WeatherForecastClientTest(CustomWebApplicationFactory<Startup> factory) : base(factory, 5743, false)
        {
        }

        [Fact]
        public async Task TestClient()
        {
            var response = await RefitClient.GetForecast();
            await response.EnsureSuccessStatusCodeAsync();
            var content = response.Content.ToList();
            Assert.Equal(5, content.Count);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        protected override IWeatherForecastClient CreateRefitClient(HttpClient httpClient)
        {
            return RestService.For<IWeatherForecastClient>(httpClient);
        }

        protected override Dictionary<string, string> GetConfiguration()
        {
            var configuration = base.GetConfiguration();
            configuration.Add("AppSettingsKey", "AppSettingsValue");
            return configuration;
        }
    }
}
