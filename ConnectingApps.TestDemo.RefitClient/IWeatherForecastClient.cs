using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace ConnectingApps.TestDemo.RefitClient
{
    public interface IWeatherForecastClient
    {
        [Get("/weatherforecast")]
        Task<ApiResponse<IEnumerable<WeatherForecast>>> GetForecast();
    }
}
