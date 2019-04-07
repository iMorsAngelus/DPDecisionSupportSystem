using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public class PriorityVectorSearcher
    {
        public async Task<double[]> SearchAsync(int size, IPairMatrix<double> inputRow)
        {
            var random = new Random();

            var functions = new EvaluationFunctions(size, inputRow, random);
            var maxPopulationCount = int.Parse(ConfigurationManager.AppSettings["MaxPopulationCount"]);
            var elitismPercentage = int.Parse(ConfigurationManager.AppSettings["ElitismPercentage"]);
            var mutationProbability = double.Parse(ConfigurationManager.AppSettings["MutationProbability"], System.Globalization.CultureInfo.InvariantCulture);
            var crossoverProbability = double.Parse(ConfigurationManager.AppSettings["CrossoverProbability"], System.Globalization.CultureInfo.InvariantCulture);
            var maxGenerationNumber = int.Parse(ConfigurationManager.AppSettings["MaxGenerationNumber"]);

            var geneticAlgorithm = new GeneticAlgorithm<byte>(maxPopulationCount, 
                8 * size,
                random,
                functions.GetRandomGene,
                functions.EvaluateFitness, 
                elitismPercentage, 
                mutationProbability, 
                crossoverProbability);

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
    }
}