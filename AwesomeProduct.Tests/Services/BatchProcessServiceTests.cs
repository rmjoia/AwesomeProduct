using System.Collections.Generic;
using AwesomeProduct.Persistence;
using AwesomeProduct.Persistence.Models;
using AwesomeProduct.Services;
using Moq;
using NUnit.Framework;

namespace AwesomeProduct.Tests.Services
{
    public class BatchProcessServiceTests
    {
        Mock<IRepository<BatchProcess>> batchProcessRepository;
        [SetUp]
        public void Setup()
        {
            batchProcessRepository = new Mock<IRepository<BatchProcess>>();
        }

        [Test]
        public void Should_Call_Repository_When_Getting_Last_Record()
        {
            //  Arrange
            var batchProcess = new BatchProcess()
            {
                Data = new List<Batch>()
            };

            batchProcessRepository.Setup(r => r.GetLast()).Returns(batchProcess);
            var batchProcessService = new BatchProcessService(batchProcessRepository.Object);

            //  Act
            var result = batchProcessService.GetLast();

            //  Assert
            batchProcessRepository.Verify(r => r.GetLast(), Times.Once);
        }

        [Test]
        public void Should_Call_Repository_When_Inserting_Record()
        {
            //  Arrange
            var item = new BatchProcess();
            var batchProcessService = new BatchProcessService(batchProcessRepository.Object);

            //  Act
            var result = batchProcessService.Insert(item);

            //  Assert
            batchProcessRepository.Verify(r => r.Insert(It.IsAny<BatchProcess>()), Times.Once);
        }


    }
}
