using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public class GeneticAlgorithm<T> : IGeneticAlgorithm<T>
    {
        public List<IChromosome<T>> Population { get; private set; }
        public IChromosome<T> BestChromosome { get; private set; }
        public int GenerationNumber { get; private set; }
        public double BestFitness { get; private set; }

        private readonly ChromosomeComparer<T> _cromosomeComparer;
        private readonly Random _random;
        private readonly Func<T> _getRandomGene;
        private readonly Func<IChromosome<T>, double> _fitnessFunction;
        private readonly int _dnaSize;
        private readonly int _elitism;
        private readonly double _mutationProbablity;
        private readonly double _crossoverProbablity;

        private List<IChromosome<T>> _newPopulation;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene,
            Func<IChromosome<T>, double> fitnessFunction,
            int elitism, double mutationProbablity = 0.01, double crossoverProbablity = 0.65)
        {
            _cromosomeComparer = new ChromosomeComparer<T>();
            GenerationNumber = 1;
            _elitism = elitism;
            _mutationProbablity = mutationProbablity;
            _crossoverProbablity = crossoverProbablity;
            Population = new List<IChromosome<T>>(populationSize);
            _newPopulation = new List<IChromosome<T>>(populationSize);
            _random = random;
            _dnaSize = dnaSize;
            _getRandomGene = getRandomGene;
            _fitnessFunction = fitnessFunction;

            for (var i = 0; i < populationSize; i++)
            {
                Population.Add(new Chromosome<T>(dnaSize, random, getRandomGene, fitnessFunction));
            }
        }

        public void NewGeneration(int newChromosomeCount = 0)
        {
            var finalCount = Population.Count + newChromosomeCount;

            if (finalCount <= 0)
            {
                return;
            }

            if (Population.Count > 0)
            {
                CalculateFitness();
                Population.Sort(_cromosomeComparer);
            }

            _newPopulation.Clear();

            for (var i = 0; i < Population.Count; i++)
            {
                if (i < _elitism && i < Population.Count)
                {
                    _newPopulation.Add(Population[i]);
                }
                else if (_random.NextDouble() < _crossoverProbablity && i < Population.Count)
                {
                    var parent1 = ChooseParent();
                    var parent2 = ChooseParent(1);

                    var child = parent1.Crossover(parent2);

                    child.Mutate(_mutationProbablity);

                    _newPopulation.Add(child);
                }
                else
                {
                    _newPopulation.Add(new Chromosome<T>(_dnaSize, _random, _getRandomGene, _fitnessFunction));
                }
            }

            var tmpList = Population;
            Population = _newPopulation;
            _newPopulation = tmpList;

            GenerationNumber++;
        }

        private void CalculateFitness()
        {
            var best = Population[0];

            foreach (var chromosome in Population)
            {
                best = chromosome.Fitness > best.Fitness ? chromosome : best;
            }

            BestFitness = best.Fitness;
            BestChromosome = best;
        }

        private IChromosome<T> ChooseParent(int defaultIndex = 0)
        {
            var randomNumber = _random.NextDouble() * Population.Sum(x => x.Fitness);

            foreach (var chromosome in Population)
            {
                if (randomNumber < chromosome.Fitness)
                {
                    return chromosome;
                }

                randomNumber -= chromosome.Fitness;
            }

            return Population[defaultIndex];
        }
    }
}