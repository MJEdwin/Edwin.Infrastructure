using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BingoCore.Infrastructure.Service.Weather
{
    public interface IWeatherService:IDisposable
    {
        Task<WeatherInfo> GetWeatherAsync(string location = null);
    }
}
