namespace DecisionSupportSystem.BusinessLogicLayer
{
    public interface IChromosome<T>
    {
        T[] Genes { get; }
        double Fitness { get; }

        IChromosome<T> Crossover(IChromosome<T> otherParent);

        void Mutate(double mutationRate);

        string ToBinaryString(int startIndex, int length);

        double[] ToDoubleArray();

        string ToString();
    }
}