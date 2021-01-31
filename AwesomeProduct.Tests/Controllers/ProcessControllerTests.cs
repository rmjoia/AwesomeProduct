using AwesomeProduct.Controllers;
using AwesomeProduct.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AwesomeProduct.Tests
{
    public class ProcessControlerTests
    {
        Mock<IBatchProcessor> batchProcessorMock;

        [SetUp]
        public void Setup()
        {
            batchProcessorMock = new Mock<IBatchProcessor>();
        }

        [Test]
        public void Should_Return_Empty_List_When_Default_Numbers_Are_Used_As_Arguments()
        {
            //  Arrange
            var processController = new ProcessController(batchProcessorMock.Object);

            //  Act
            var result = processController.Get();

            //  Assert

            Assert.That(result, Is.TypeOf<BadRequestResult>());
            batchProcessorMock.Verify(p => p.Process(0, 0), Times.Never());
        }

        [Test]
        public void Should_Return_Empty_List_When_Zeroes_Are_Used_As_Arguments()
        {
            //  Arrange
            var processController = new ProcessController(batchProcessorMock.Object);

            //  Act
            var result = processController.Get(0, 0);

            //  Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            batchProcessorMock.Verify(p => p.Process(0, 0), Times.Never());
        }

        [Test]
        public void Should_Return_Empty_List_When_Negative_Numbers_Are_Used_As_Arguments()
        {
            //  Arrange
            var processController = new ProcessController(batchProcessorMock.Object);

            //  Act
            var result = processController.Get(-5, -6);

            //  Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            batchProcessorMock.Verify(p => p.Process(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }

        [Test]
        public void Should_Call_Generate_When_Valid_NumbersToProcess_Parameter_Is_Used()
        {
            //  Arrange
            var processController = new ProcessController(batchProcessorMock.Object);

            //  Act
            var result = processController.Get(1, 1);

            //  Assert
            Assert.That(result, Is.TypeOf<OkResult>());
            batchProcessorMock.Verify(p => p.Process(1, 1), Times.Once());
        }
    }
}