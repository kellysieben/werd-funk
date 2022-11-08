using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using common;

namespace api
{
    public static class Weather
    {
        [FunctionName("WeatherForecast")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var randomNumber = new Random();
            var temp = 0;

            log.LogInformation("JBF : API : GET WeatherForecast");

            var result = Enumerable.Range(1, 50).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = temp = randomNumber.Next(-20, 55)
            }).ToArray();

            return new OkObjectResult(result);
        }
    }
}
