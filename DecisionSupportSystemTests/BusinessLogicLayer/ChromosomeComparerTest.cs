using DecisionSupportSystem.BusinessLogicLayer;
using NUnit.Framework;
using System;

namespace DecisionSupportSystemTests.BusinessLogicLayer
{
    [TestFixture]
    class ChromosomeComparerTest
    {
        private ChromosomeComparer<double> unit;

        [SetUp]
        public void SetUp()
        {
            unit = new ChromosomeComparer<double>();
        }

        [Test]
        public void CompareTwoChromosome_FirstGreater()
        {
            //GIVEN
            var rnd = new Random();
            var firstChromosome = new Chromosome<double>(24, rnd, GetRandomGene, getFirstFitness);
            var secondChromosome = new Chromosome<double>(24, rnd, GetRandomGene, getSecondFitness);

            //WHEN
            var result = unit.Compare(firstChromosome, secondChromosome);

            //THEN
            Assert.AreEqual(result, -1);
        }

        [Test]
        public void CompareTwoChromosome_BothEquals()
        {
            //GIVEN
            var rnd = new Random();
            var firstChromosome = new Chromosome<double>(24, rnd, GetRandomGene, getFirstFitness);
            var secondChromosome = new Chromosome<double>(24, rnd, GetRandomGene, getFirstFitness);

            //WHEN
            var result = unit.Compare(firstChromosome, secondChromosome);

            //THEN
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void CompareTwoChromosome_FirstLess()
        {
            //GIVEN
            var rnd = new Random();
            var firstChromosome = new Chromosome<double>(24, rnd, GetRandomGene, getSecondFitness);
            var secondChromosome = new Chromosome<double>(24, rnd, GetRandomGene, getFirstFitness);

            //WHEN
            var result = unit.Compare(firstChromosome, secondChromosome);

            //THEN
            Assert.AreEqual(result, 1);
        }

        private double getFirstFitness(IChromosome<double> chromosome)
        {
            return 1;
        }

        private double getSecondFitness(IChromosome<double> chromosome)
        {
            return 0;
        }

        private double GetRandomGene()
        {
            return 1;
        }
    }
}
