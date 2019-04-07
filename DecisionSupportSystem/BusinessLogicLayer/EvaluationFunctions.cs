using System;
using System.Configuration;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    class EvaluationFunctions
    {
        private int _size;
        private IPairMatrix<double> _inputRow;
        private Random _random;

        public EvaluationFunctions(int size, IPairMatrix<double> inputRow, Random random)
        {
            _size = size;
            _inputRow = inputRow;
            _random = random;
        }

        public byte GetRandomGene() => (byte)_random.Next(0, 2);

        public double EvaluateFitness(IChromosome<byte> chromosome)
        {
            double fitnessValue = 9;

            if (chromosome == null)
                throw new ArgumentNullException(nameof(chromosome), @"The specified Chromosome is null.");

            var numbers = chromosome.ToDoubleArray();

            for (var i = 0; i < _size; i++)
            {
                double w = -1;
                for (var j = 0; j < _size; j++)
                {
                    if (i == j) continue;

                    if (numbers[i] / numbers[j] <= _inputRow.Matrix[i, j].Numbers[1])
                    {
                        w = Math.Round(
                            (numbers[i] / numbers[j] - _inputRow.Matrix[i, j].Numbers[0]) /
                            (_inputRow.Matrix[i, j].Numbers[1] - _inputRow.Matrix[i, j].Numbers[0]),
                            int.Parse(ConfigurationManager.AppSettings["CalculationAccuracy"]),
                            MidpointRounding.AwayFromZero
                        );


                    }
                    else if (numbers[i] / numbers[j] > _inputRow.Matrix[i, j].Numbers[1])
                    {
                        w = Math.Round(
                            (_inputRow.Matrix[i, j].Numbers[2] - numbers[i] / numbers[j]) /
                            (_inputRow.Matrix[i, j].Numbers[2] - _inputRow.Matrix[i, j].Numbers[1]),
                            int.Parse(ConfigurationManager.AppSettings["CalculationAccuracy"]),
                            MidpointRounding.AwayFromZero
                        );

                    }

                    w = double.IsInfinity(w) ? 1 : w;
                    fitnessValue = fitnessValue > w ? w : fitnessValue;
                }
            }

            return fitnessValue;
        }
    }
}
