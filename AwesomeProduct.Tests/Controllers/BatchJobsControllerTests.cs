using AwesomeProduct.Controllers;
using AwesomeProduct.Persistence.Models;
using AwesomeProduct.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AwesomeProduct.Tests
{
    public class BatchJobsControllerTests
    {
        Mock<IBatchProcessService> batchProcessServiceMock;

        [SetUp]
        public void Setup()
        {
            batchProcessServiceMock = new Mock<IBatchProcessService>();
        }

        [Test]
        public void Should_Return_Empty_When_No_Records_Are_Found()
        {
            //  Arrange
            batchProcessServiceMock.Setup(r => r.GetLast()).Returns<BatchProcess>(null);
            var batchJobsController = new BatchJobsController(batchProcessServiceMock.Object);

            //  Act
            var result = batchJobsController.Get();

            //  Assert

            Assert.That(result, Is.TypeOf<NotFoundResult>());
            batchProcessServiceMock.Verify(r => r.GetLast(), Times.Once());
        }

        [Test]
        public void Should_Return_Result_When_Records_Is_Found()
        {
            //  Arrange
            batchProcessServiceMock.Setup(r => r.GetLast()).Returns(new BatchProcess());
            var batchJobsController = new BatchJobsController(batchProcessServiceMock.Object);

            //  Act
            var result = batchJobsController.Get();

            //  Assert

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            batchProcessServiceMock.Verify(r => r.GetLast(), Times.Once());
        }

        [Test]
        public void Should_Return_BadRequestResult_When_No_Record_Is_Inserted()
        {
            //  Arrange
            batchProcessServiceMock.Setup(r => r.Insert(It.IsAny<BatchProcess>())).Returns(0);
            var batchJobsController = new BatchJobsController(batchProcessServiceMock.Object);
            var response = new BatchProcess();
            //  Act
            var result = batchJobsController.Post(response);

            //  Assert

            Assert.That(result, Is.TypeOf<BadRequestResult>());
            batchProcessServiceMock.Verify(r => r.Insert(It.IsAny<BatchProcess>()), Times.Once());
        }

        [Test]
        public void Should_Return_Created_When_Record_Is_Inserted()
        {
            //  Arrange
            batchProcessServiceMock.Setup(r => r.Insert(It.IsAny<BatchProcess>())).Returns(1);
            var batchJobsController = new BatchJobsController(batchProcessServiceMock.Object);
            var response = new BatchProcess();
            //  Act
            var result = batchJobsController.Post(response);

            //  Assert

            Assert.That(result, Is.TypeOf<StatusCodeResult>());
            batchProcessServiceMock.Verify(r => r.Insert(It.IsAny<BatchProcess>()), Times.Once());
        }
    }
}