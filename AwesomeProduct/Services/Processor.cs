using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeProduct.Models;

namespace AwesomeProduct.Services
{
    public class GeneratorManager : IGeneratorManager
    {
        private Random randomGenerator = new Random(new DateTime().Millisecond);
        const int secondsInMilliseconds = 1000;

        public GeneratorManager()
        {
        }

        public event IGeneratorManager.NumberGeneratedEventHandler NumberGenerated;

        public void Generate(int batchNumber, int numbersToGenerate)
        {
            Enumerable.Range(0, numbersToGenerate).ToList().ForEach(async _ =>
            {
                await Task.Delay(getRandomNumber(1, 5) * secondsInMilliseconds);
                NumberGenerated?.Invoke(new BatchJob(batchNumber, randomGenerator.Next(1, 100), numbersToGenerate));
            });
        }

        public BatchJob Multiply(BatchJob batchJob)
        {
            var randomMultiplier = getRandomNumber(2, 4);
            var randomDelayInMiliseconds = getRandomNumber(1, 5) * secondsInMilliseconds;

            Task.Delay(randomDelayInMiliseconds).Wait();

            return new BatchJob(batchJob.BatchNumber, batchJob.Number * randomMultiplier, batchJob.LeftToProcess);
        }

        private int getRandomNumber(int startValue, int endValue)
        {
            return randomGenerator.Next(startValue, endValue);
        }
    }
}