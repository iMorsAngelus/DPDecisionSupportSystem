using DecisionSupportSystem.BusinessLogicLayer;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace DecisionSupportSystemTests.BusinessLogicLayer
{
    [TestFixture]
    public class ChromosomeTest
    {
        private Mock<Random> mockRandom;
        private Mock<Func<double>> mockGetRandomGene;
        private Mock<Func<IChromosome<double>, double>> mockFitnessFunction;
        private Chromosome<double> unit;

        [SetUp]
        public void SetUp()
        {
            //Initialize
            mockRandom = new Mock<Random>();
            mockGetRandomGene = new Mock<Func<double>>();
            mockFitnessFunction = new Mock<Func<IChromosome<double>, double>>();

            //Mock setup
            mockGetRandomGene.Setup(m => m.Invoke()).Returns(1);

            //Create unit to test
            unit = new Chromosome<double>(24, mockRandom.Object, mockGetRandomGene.Object, mockFitnessFunction.Object);
        }

        [TestCase(0.6, 0.5)]
        [TestCase(0.4, 1)]
        [Test]
        public void Chrmosome_Crossover_Test(double randomValue, double geneValue)
        {
            //GIVEN
            mockGetRandomGene.Setup(m => m.Invoke()).Returns(0.5);
            var parentChromosome = new Chromosome<double>(24, mockRandom.Object, mockGetRandomGene.Object, mockFitnessFunction.Object);

            //WHEN
            mockRandom.Setup(m => m.NextDouble()).Returns(randomValue);
            var newChromosome = unit.Crossover(parentChromosome);

            //THEN
            Assert.True(newChromosome.Genes.All(g => g.Equals(geneValue)));
            mockGetRandomGene.Verify(m => m.Invoke(), Times.Exactly(48));
        }

        [TestCase(0.6, 24)]
        [TestCase(0.4, 48)]
        [Test]
        public void Chrmosome_Mutate_Test(double randomValue, int randomGeneCount)
        {
            //GIVEN
            var mutationRate = 0.5;

            //WHEN
            mockRandom.Setup(m => m.NextDouble()).Returns(randomValue);
            unit.Mutate(mutationRate);

            //THEN
            Assert.True(unit.Genes.All(g => g.Equals(1)));
            mockGetRandomGene.Verify(m => m.Invoke(), Times.Exactly(randomGeneCount));
        }
    }
}
