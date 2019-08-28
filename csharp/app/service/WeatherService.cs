
using System;
using System.Net.Http;
using System.Threading.Tasks;
using advancedbackend.domain.apiresponse.cityinfo;
using advancedbackend.domain.config;
using Microsoft.Extensions.Options;

namespace advancedbackend.services
{
    public interface IWeatherService {
        Task<double> GetTemperatureByCityName(string cityname);
        Task<double> GetTemperatureByCoord(float lat, float lon);
    }
    public class WeatherService : IWeatherService
    {
        private IHttpClientFactory ClientFactory;
        private IOptions<AppSettings> Config;
        public WeatherService(IHttpClientFactory clientFactory, IOptions<AppSettings> config)
        {
            ClientFactory = clientFactory;
            Config = config;
        }

        public async Task<double> GetTemperatureByCityName(string cityname)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.openweathermap.org/data/2.5/weather?q={cityname}&appid={Config.Value.weather}");
            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content
                    .ReadAsStringAsync();
                var info = CityInfo.FromJson(json);

                var kelTemp = info.Main.Temp;
                //From Kelvin to Celsius
                var celTemp = kelTemp - 273.15;
                return celTemp;

            }else {
                throw new ApplicationException(response.StatusCode.ToString());
            }
        }

        public async Task<double> GetTemperatureByCoord(float lat, float lon)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={Config.Value.weather}");
            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content
                    .ReadAsStringAsync();
                var info = CityInfo.FromJson(json);

                var farTemp = info.Main.Temp;
                //(32°F − 32) × 5/9 = 0°C
                var celTemp = (farTemp - 32) * 5/9;
                return celTemp;

            }else {
                throw new ApplicationException(response.StatusCode.ToString());
            }
        }
    }
}