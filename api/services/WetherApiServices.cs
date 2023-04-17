using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Services
{
    public class WeatherApiClient
    {
        private readonly string apiKey;

        public WeatherApiClient(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<string> GetWeather(string zipcode)
        {
            using var httpClient = new HttpClient();

            var url = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={zipcode}&aqi=yes";

            var response = await httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var json = JsonDocument.Parse(content);
                var location = json.RootElement.GetProperty("location");
                var region = location.GetProperty("region").GetString();
                var country = location.GetProperty("country").GetString();
                return $"{region}, {country}";
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve weather data. Status code: {response.StatusCode}");
            }
        }
    }

}
