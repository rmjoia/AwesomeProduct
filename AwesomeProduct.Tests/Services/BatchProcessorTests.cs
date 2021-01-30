
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeProduct.Models;
using AwesomeProduct.Services;
using Moq;
using NUnit.Framework;

namespace AwesomeProduct.Tests.Services
{
    public class BatchProcessorTests
    {
        Mock<IGeneratorManager> processorMock;
        [SetUp]
        public void Setup()
        {
            processorMock = new Mock<IGeneratorManager>();
        }

        [Test]
        public void Should_Return_Result_And_Status_Completed_Empty_When_Zeroes_Are_Used_As_Parameters()
        {
            //  Arrange
            var batchProcessor = new BatchProcessor(processorMock.Object);

            //  Act
            var result = batchProcessor.Process(0, 0);

            //  Assert
            Assert.That(result.Data, Is.Empty);
            Assert.That(result.IsComplete, Is.True);
        }

        [Test]
        public void Should_Return_Result_And_Status_Completed_When_ones_Are_Used_As_Parameters()
        {
            //  Arrange
            var batchProcessor = new BatchProcessor(processorMock.Object);
            processorMock.Setup(p => p.Multiply(It.IsAny<BatchJob>())).Returns(new BatchJob(1, 2));
            processorMock.Setup(p => p.Generate(It.IsAny<int>(), It.IsAny<int>()))
            .Callback(() =>
            {
                processorMock.Raise(p => p.NumberGenerated += null, new BatchJob(1, 2));
            });

            var expectedResult = new List<BatchJob>()
            {
                new BatchJob(1,2)
            };

            //  Act
            var result = batchProcessor.Process(1, 1);

            //  Assert
            Assert.That(result.Data.Select(d => new { d.BatchNumber, d.Number }), Is.EquivalentTo(expectedResult.Select(d => new { d.BatchNumber, d.Number })));
            Assert.That(result.IsComplete, Is.True);
        }

        [Test]
        [Ignore("This test started to fail when running in combination with others")]
        public void Should_Return_Result_And_Status_Incompleted_When_Still_Processing()
        {
            //  Arrange
            var batchProcessor = new BatchProcessor(processorMock.Object);
            processorMock.Setup(p => p.Generate(It.IsAny<int>(), It.IsAny<int>())).Callback(async () => await Task.Delay(500));

            //  Act
            Task.Run(() => batchProcessor.Process(1, 1));
            var result = batchProcessor.getStatus();

            //  Assert
            Assert.That(result.Data, Is.Empty);
            Assert.That(result.IsComplete, Is.False);
        }
    }
}
