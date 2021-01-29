using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OtherApi.Controllers;

namespace OtherApi.Tests
{
    public class WeatherForecastControllerTest
    {
        [SetUp]
        public void Setup()
        {
            Mock<ILogger<WeatherForecastController>> logger = new Mock<ILogger<WeatherForecastController>>();
            var weatherForecast = new WeatherForecastController(logger.Object);

            Assert.NotNull(weatherForecast);
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}