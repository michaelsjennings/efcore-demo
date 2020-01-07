using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
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

            List<Task> tasks = new List<Task>();

            for (var i = 0; i < 1000; i++)
            {
                tasks.Add(CreateUpdateAndDeleteEventThroughApi());
            }

            await Task.WhenAll(tasks);
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
                        Title = Faker.Company.CatchPhrase(),
                        Date = Faker.Date.Past(),
                        LocationDescription =
                            Faker.Number.RandomNumber(1, 9999) + " " +
                            Faker.Address.StreetName() + " " +
                            Faker.Address.StreetSuffix() + ", " +
                            Faker.Address.USCity() + ", " +
                            Faker.Address.StateAbbreviation() + " " +
                            Faker.Address.USZipCode()
                    });

                var postResponse = await client.PostAsync(eventsApiBaseUrl, new StringContent(postRequestJson, Encoding.UTF8, MediaTypeNames.Application.Json));
                if (postResponse.StatusCode != HttpStatusCode.Created)
                {
                    Console.WriteLine($"ERROR CREATING EVENT!");
                }
                else
                {
                    var postResponseContent = await postResponse.Content.ReadAsStringAsync();
                    var postReponseData = JObject.Parse(postResponseContent);

                    var eventId = postReponseData["eventId"].Value<int>();
                    Console.WriteLine($"Created event Id {eventId}");

                    await RandomDelay();

                    var putRequestJson = JsonConvert.SerializeObject(
                        new
                        {
                            Title = Faker.Company.CatchPhrase(),
                            Date = Faker.Date.Past(),
                            LocationDescription =
                                Faker.Number.RandomNumber(1, 9999) + " " +
                                Faker.Address.StreetName() + " " +
                                Faker.Address.StreetSuffix() + ", " +
                                Faker.Address.USCity() + ", " +
                                Faker.Address.StateAbbreviation() + " " +
                                Faker.Address.USZipCode()
                        });

                    var putResponse = await client.PutAsync($"{eventsApiBaseUrl}/{eventId}", new StringContent(putRequestJson, Encoding.UTF8, MediaTypeNames.Application.Json));
                    if (putResponse.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine($"ERROR UPDATING EVENT ID {eventId}!");
                    }
                    else
                    {
                        Console.WriteLine($"Updated event Id {eventId}");

                        await RandomDelay();

                        var deleteReponse = await client.DeleteAsync($"{eventsApiBaseUrl}/{eventId}");
                        if (deleteReponse.StatusCode != HttpStatusCode.OK)
                        {
                            Console.WriteLine($"ERROR DELETING EVENT ID {eventId}!");
                        }
                        else
                        {
                            Console.WriteLine($"Deleted event Id {eventId}");
                        }
                    }
                }
            }
        }
    }
}
