using System.Collections.Generic;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public interface IGeneticAlgorithm<T>
    {
        List<IChromosome<T>> Population { get; }
        IChromosome<T> BestChromosome { get; }
        int GenerationNumber { get; }
        double BestFitness { get; }


        void NewGeneration(int newChromosomeCount = 0);
    }
}