using AwesomeProduct.Persistence;
using AwesomeProduct.Persistence.Models;
using AwesomeProduct.Services;
using Moq;
using NUnit.Framework;

namespace AwesomeProduct.Tests.Services
{
    public class BatchProcessServiceTests
    {
        Mock<IRepository<BatchProcess>> batchProessRepository;
        [SetUp]
        public void Setup()
        {
            batchProessRepository = new Mock<IRepository<BatchProcess>>();
        }

        [Test]
        public void Should_Call_Repository_When_Getting_Last_Record()
        {
            //  Arrange
            var batchProcessService = new BatchProcessService(batchProessRepository.Object);

            //  Act
            var result = batchProcessService.GetLast();

            //  Assert
            batchProessRepository.Verify(r => r.GetLast(), Times.Once);
        }

        [Test]
        public void Should_Call_Repository_When_Inserting_Record()
        {
            //  Arrange
            var item = new BatchProcess();
            var batchProcessService = new BatchProcessService(batchProessRepository.Object);

            //  Act
            var result = batchProcessService.Insert(item);

            //  Assert
            batchProessRepository.Verify(r => r.Insert(It.IsAny<BatchProcess>()), Times.Once);
        }


    }
}
