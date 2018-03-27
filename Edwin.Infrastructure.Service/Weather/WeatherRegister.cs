using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BingoCore.Infrastructure.Service.Weather
{
    public static class WeatherRegister
    {
        public static IServiceCollection AddWeatherApi(this IServiceCollection service, Action<WeatherConfigure> action)
        {
            service.Configure(action);
            service.AddSingleton<IWeatherService, WeatherService>();
            return service;
        }
    }
}
