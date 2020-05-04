using ConnectingApps.TestDemo.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectingApps.IntegrationTests
{
    public class CustomWebapplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureServices(sc => sc.AddTransient(typeof(WeatherForecastController),typeof(WeatherForecastController)));
            builder.ConfigureServices(sc => sc.AddTransient<WeatherForecastController, WeatherForecastController>());
            base.ConfigureWebHost(builder);
        }
    }
}
