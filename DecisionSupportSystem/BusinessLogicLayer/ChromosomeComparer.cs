using System.Collections.Generic;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public class ChromosomeComparer<T> : IComparer<IChromosome<T>>
    {
        public int Compare(IChromosome<T> a, IChromosome<T> b) =>
            a?.Fitness > b?.Fitness ? -1 : (a?.Fitness < b?.Fitness ? 1 : 0);
    }
}