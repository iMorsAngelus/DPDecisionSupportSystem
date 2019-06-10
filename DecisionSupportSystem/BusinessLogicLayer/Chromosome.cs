using System;
using System.Configuration;
using System.Linq;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public class Chromosome<T> : IChromosome<T>
    {
        public T[] Genes { get; }
        public double Fitness => _fitnessFunction(this);

        private readonly Random _random;
        private readonly Func<T> _getRandomGene;
        private readonly Func<IChromosome<T>, double> _fitnessFunction;

        public Chromosome(int size, Random random, Func<T> getRandomGene, Func<IChromosome<T>, double> fitnessFunction,
            bool shouldInitGenes = true)
        {
            Genes = new T[size];
            _random = random;
            _getRandomGene = getRandomGene;
            _fitnessFunction = fitnessFunction;

            if (!shouldInitGenes) return;

            for (var i = 0; i < Genes.Length; i++)
            {
                Genes[i] = getRandomGene();
            }
        }

        public IChromosome<T> Crossover(IChromosome<T> otherParent)
        {
            var child = new Chromosome<T>(Genes.Length, _random, _getRandomGene, _fitnessFunction, false);

            for (var i = 0; i < Genes.Length; i++)
            {
                child.Genes[i] = _random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
            }

            return child;
        }

        public void Mutate(double mutationRate)
        {
            for (var i = 0; i < Genes.Length; i++)
            {
                if (_random.NextDouble() < mutationRate)
                {
                    Genes[i] = _getRandomGene();
                }
            }
        }

        public string ToBinaryString(int startIndex, int length)
        {
            return string.Join("", Genes.ToList().GetRange(startIndex, length));
        }

        public double[] ToDoubleArray()
        {
            if (typeof(T) != typeof(byte)) return new[] {Double.NaN};

            var dimensionSize = (int) (Genes.Length / 8);

            var temp = new double[dimensionSize];
            double sum = 0;
            for (var i = 0; i < dimensionSize; i++)
            {
                temp[i] = BitConverter.Int64BitsToDouble(Convert.ToInt64(ToBinaryString(i * 8, 8), 2));
                sum += temp[i];
            }

            var convertedNumbers = new double[dimensionSize];
            for (var i = 0; i < dimensionSize; i++)
            {
                try
                {

                    convertedNumbers[i] = Math.Round((double) temp[i] / sum,
                        int.Parse(ConfigurationManager.AppSettings["CalculationAccuracy"]?? "3"),
                        MidpointRounding.AwayFromZero);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return convertedNumbers;
        }

        public override string ToString()
        {
            var numbers = ToDoubleArray();
            return $"W1 = {numbers[0]}, W2 = {numbers[1]}, W3 = {numbers[2]};  Fitness = {Fitness}";
        }
    }
}