using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeProduct.Models;

namespace AwesomeProduct.Services
{
    public class GeneratorManager : IGeneratorManager
    {
        private Random rng = new Random(new DateTime().Millisecond);

        public GeneratorManager()
        {
        }

        public event IGeneratorManager.NumberGeneratedEventHandler NumberGenerated;

        public void Generate(int batchNumber, int numbersToGenerate)
        {

            Enumerable.Range(0, numbersToGenerate).ToList().ForEach(async _ =>
           {
               await Task.Delay(5000);
               NumberGenerated?.Invoke(new BatchJob(batchNumber, rng.Next(1, 100), numbersToGenerate));
           });
        }

        public BatchJob Multiply(BatchJob batchJob)
        {
            return new BatchJob(batchJob.BatchNumber, batchJob.Number * 2, batchJob.LeftToProcess);
        }
    }
}