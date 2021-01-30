using System;
using System.Collections.Generic;
using AwesomeProduct.Models;

namespace AwesomeProduct.Services
{
    public interface IGeneratorManager
    {

        delegate void NumberGeneratedEventHandler(BatchJob job);
        event NumberGeneratedEventHandler NumberGenerated;
        void Generate(int batch, int numbersToGenerate);
        BatchJob Multiply(BatchJob batchJob);
    }
}
