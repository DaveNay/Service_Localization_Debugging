using Service_Localization_Debugging.Data;
using System;

namespace Service_Localization_Debugging.Services
{
    public class WeatherCache
    {
        public event Action<WeatherForecast[]> ForecastHasChanged;

        public WeatherForecast[] Forecast { get; private set; }

        public void Update(WeatherForecast[] forecast)
        {
            Forecast = forecast;
            ForecastHasChanged?.Invoke(Forecast);
        }
    }
}