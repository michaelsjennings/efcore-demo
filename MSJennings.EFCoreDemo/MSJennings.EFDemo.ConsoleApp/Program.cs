using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MSJennings.EFDemo.ConsoleApp
{
    class Program
    {
        static Random _random;
        static ServiceProvider _serviceProvider;
        static IHttpClientFactory _httpClientFactory;

        static async Task Main(string[] args)
        {
            _random = new Random();
            _serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            _httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();

            await CreateUpdateAndDeleteEventThroughApi();
        }

        static async Task RandomDelay()
        {
            var milliseconds = _random.Next(500, 2000);
            await Task.Delay(milliseconds);
        }

        static async Task CreateUpdateAndDeleteEventThroughApi()
        {
            var eventsApiBaseUrl = "https://localhost:44374/api/Events";

            using (var client = _httpClientFactory.CreateClient())
            {
                await RandomDelay();

                var postRequestJson = JsonConvert.SerializeObject(
                    new
                    {
                        Title = "Some Title",
                        Date = DateTime.Today,
                        LocationDescription = "Some Location"
                    });

                var postResponse = await client.PostAsync(eventsApiBaseUrl, new StringContent(postRequestJson, Encoding.UTF8, MediaTypeNames.Application.Json));
                var postResponseContent = await postResponse.Content.ReadAsStringAsync();
                var postReponseData = JObject.Parse(postResponseContent);

                var eventId = postReponseData["eventId"].Value<int>();

                await RandomDelay();

                var putRequestJson = JsonConvert.SerializeObject(
                    new
                    {
                        Title = "Some Other Title",
                        Date = DateTime.Today.AddDays(1),
                        LocationDescription = "Some Other Location"
                    });

                await client.PutAsync($"{eventsApiBaseUrl}/{eventId}", new StringContent(putRequestJson, Encoding.UTF8, MediaTypeNames.Application.Json));

                await RandomDelay();

                await client.DeleteAsync($"{eventsApiBaseUrl}/{eventId}");
            }
        }
    }
}
