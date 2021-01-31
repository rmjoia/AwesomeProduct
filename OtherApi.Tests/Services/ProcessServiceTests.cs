using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using OtherApi.Services;

namespace OtherApi.Tests
{
    public class ProcessServiceTest
    {
        private Mock<IHttpClientFactory> httpClientFactoryMock;

        [SetUp]
        public void Setup()
        {
            httpClientFactoryMock = new Mock<IHttpClientFactory>();

        }

        [Test]
        public async Task Should_Return_Status_Code_OK()
        {
            //  Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}"),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var processService = new ProcessService(httpClientFactoryMock.Object);

            //  Act
            var result = await processService.StartProcessingAsync(1, 1);

            //  Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Should_Return_Status_Code_BadRequest()
        {
            //  Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("{}"),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var processService = new ProcessService(httpClientFactoryMock.Object);

            //  Act
            var result = await processService.StartProcessingAsync(1, 1);

            //  Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Should_Return_Status_Code_NotFound()
        {
            //  Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("{}"),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var processService = new ProcessService(httpClientFactoryMock.Object);

            //  Act
            var result = await processService.StartProcessingAsync(1, 1);

            //  Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}