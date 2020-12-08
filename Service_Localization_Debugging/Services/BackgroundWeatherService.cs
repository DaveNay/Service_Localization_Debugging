using Microsoft.Extensions.Hosting;
using Service_Localization_Debugging.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service_Localization_Debugging.Services
{
    public class BackgroundWeatherService : BackgroundService
    {
        private readonly WeatherCache _weatherCache;
        private readonly WeatherForecastService _weatherForecastService;

        public BackgroundWeatherService(WeatherForecastService weatherForecastService, WeatherCache weatherCache)
        {
            _weatherForecastService = weatherForecastService;
            _weatherCache = weatherCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var forecast = await _weatherForecastService.GetForecastAsync(DateTime.Now);
            _weatherCache.Update(forecast);

            // Wait ten seconds after starting before continuous updates
            await Task.Delay(10000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(100, stoppingToken);
                forecast = await _weatherForecastService.GetForecastAsync(DateTime.Now);
                _weatherCache.Update(forecast);
            }
        }
    }
}