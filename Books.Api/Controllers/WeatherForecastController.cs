using Books.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();

            var client = new RestClient("https://my-json-server.typicode.com/hikdogru/angular-app/products");
            client.Timeout = -1;
            var request =  new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            var content =  response.Content;
            var jsonContent =  await JsonSerializer.DeserializeAsync<List<Book>>(new MemoryStream(Encoding.UTF8.GetBytes(content)) , new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return jsonContent;

        }
    }
}
