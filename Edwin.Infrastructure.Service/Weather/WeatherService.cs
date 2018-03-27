using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BingoCore.Infrastructure.Service.Weather
{
    public class WeatherService : IWeatherService
    {
        private const string BaseUrl = "https://api.seniverse.com/v3/weather/now.json";

        private static readonly HttpClient _httpClient;

        private WeatherConfigure _configure;

        static WeatherService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        public WeatherService(IOptions<WeatherConfigure> options)
        {
            _configure = options.Value;
        }

        public async Task<WeatherInfo> GetWeatherAsync(string location = null)
        {
            var result = await _httpClient.GetStringAsync(BaseUrl + "?" + _configure.ToUrlParameter());
            JObject obj = JObject.Parse(result);
            return JsonConvert.DeserializeObject<WeatherInfo>(obj["results"][0]["now"].ToString());
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
