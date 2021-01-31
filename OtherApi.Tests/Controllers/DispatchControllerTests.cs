using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OtherApi.Controllers;
using OtherApi.Services;

namespace OtherApi.Tests
{
    public class DispatchControllerTest
    {
        private Mock<IProcessService> processServiceMock;

        [SetUp]
        public void Setup()
        {
            processServiceMock = new Mock<IProcessService>();

        }

        [Test]
        public async Task Get_Should_Not_Call_Process_Service_When_No_Arguments_Are_Used()
        {
            //  Arrange
            var dispatchController = new DispatchController(processServiceMock.Object);

            //  Act
            var result = await dispatchController.Get();

            //  Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            processServiceMock
                .Verify(
                p => p.StartProcessingAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never());
        }

        [Test]
        public async Task Get_Should_Not_Call_Process_Service_When_Invalid_Arguments_Are_Used()
        {
            //  Arrange
            var dispatchController = new DispatchController(processServiceMock.Object);

            //  Act
            var result = await dispatchController.Get(-1, -50);

            //  Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            processServiceMock
                .Verify(
                p => p.StartProcessingAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never());
        }


        [Test]
        public async Task Get_Should_Call_Process_Service_When_Valid_Arguments_Are_Used()
        {
            //  Arrange
            processServiceMock
                .Setup(s => s.StartProcessingAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new HttpResponseMessage()));
            var dispatchController = new DispatchController(processServiceMock.Object);

            //  Act
            var result = await dispatchController.Get(1, 1);

            //  Assert
            Assert.That(result, Is.TypeOf<OkResult>());
            processServiceMock
                .Verify(
                p => p.StartProcessingAsync(
                    It.Is<int>(a => a == 1),
                    It.Is<int>(a => a == 1)),
                Times.Once());
        }


        [Test]
        public async Task GetStatus_Should_Call_Process_Service_When_Valid_Arguments_Are_Used()
        {
            //  Arrange
            processServiceMock
                .Setup(s => s.GetCurrentStatusAsync())
                .Returns(Task.FromResult(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}"),
                }));
            var dispatchController = new DispatchController(processServiceMock.Object);

            //  Act
            var result = await dispatchController.GetStatus();

            //  Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            processServiceMock.Verify(p => p.GetCurrentStatusAsync(), Times.Once());
            processServiceMock
                .Verify(
                p => p.StartProcessingAsync(
                    It.Is<int>(a => a == 1),
                    It.Is<int>(a => a == 1)),
                Times.Never());
        }

        [Test]
        public async Task GetStatus_Should_Not_Call_Process_Service_When_Valid_Arguments_Are_Used()
        {
            //  Arrange
            processServiceMock
                .Setup(s => s.GetCurrentStatusAsync())
                .Returns(Task.FromResult(new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest }));
            var dispatchController = new DispatchController(processServiceMock.Object);

            //  Act
            var result = await dispatchController.GetStatus();

            //  Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            processServiceMock.Verify(p => p.GetCurrentStatusAsync(), Times.Once());
            processServiceMock
                .Verify(
                p => p.StartProcessingAsync(
                    It.Is<int>(a => a == 1),
                    It.Is<int>(a => a == 1)),
                Times.Never());
        }
    }
}