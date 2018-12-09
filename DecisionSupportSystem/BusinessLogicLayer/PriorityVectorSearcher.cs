using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public class PriorityVectorSearcher
    {
        private Random _random;
        private int _size;
        private IPairMatrix<double> _inputRow;

        public async Task<double[]> SearchAsync(int size, IPairMatrix<double> inputRow,
            double crossoverProbability = 0.65,
            double mutationProbability = 0.01, int elitismPercentage = 2, int maxGenerationNumber = 50,
            int maxPopulationCount = 100)
        {

            _random = new Random();
            _size = size;
            _inputRow = inputRow;

            var geneticAlgorithm = new GeneticAlgorithm<byte>(maxPopulationCount, 8 * size, _random,
                GetRandomGene,
                EvaluateFitness, elitismPercentage, mutationProbability, crossoverProbability);

            var searchingTask = Task.Factory.StartNew(() =>
                {
                    for (var i = 0; i < maxGenerationNumber && geneticAlgorithm.BestFitness < 1; i++)
                    {
                        geneticAlgorithm.NewGeneration();
                    }

                    return geneticAlgorithm.BestChromosome.ToDoubleArray();
                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)
                .ConfigureAwait(false);

            return await searchingTask;
        }

        private byte GetRandomGene() => (byte) _random.Next(0, 2);

        private double EvaluateFitness(IChromosome<byte> chromosome)
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