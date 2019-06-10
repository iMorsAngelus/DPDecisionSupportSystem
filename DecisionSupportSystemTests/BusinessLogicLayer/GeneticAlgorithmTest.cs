using DecisionSupportSystem.BusinessLogicLayer;
using Moq;
using NUnit.Framework;
using System;

namespace DecisionSupportSystemTests.BusinessLogicLayer
{
    [TestFixture]
    class GeneticAlgorithmTest
    {
        private GeneticAlgorithm<byte> unit;
        private Mock<Random> mockRandom;
        private Mock<Func<byte>> mockGetRandomGene;
        private Mock<Func<IChromosome<byte>, double>> mockFitnessFunction;

        [SetUp]
        public void SetUp()
        {
            mockRandom = new Mock<Random>();
            mockGetRandomGene = new Mock<Func<byte>>();
            mockFitnessFunction = new Mock<Func<IChromosome<byte>, double>>();
            var rnd = new Random();

            mockGetRandomGene.Setup(m => m.Invoke()).Returns(() => { return (byte)rnd.Next(0, 2); });

            unit = new GeneticAlgorithm<byte>(100, 
                24, 
                mockRandom.Object, 
                mockGetRandomGene.Object, 
                mockFitnessFunction.Object, 5);
        }

        [TestCase(0.5)]
        [TestCase(1)]
        [Test]
        public void NewGenerationSuccessfull(double fitness)
        {
            //GIVEN
            mockRandom.Setup(m => m.NextDouble()).Returns(0.1);
            mockFitnessFunction.Setup(m => m.Invoke(It.IsAny<IChromosome<byte>>())).Returns(fitness);

            //WHEN
            unit.NewGeneration();

            //THEN
            Assert.AreEqual(unit.GenerationNumber, 2);
            Assert.AreEqual(unit.BestFitness, fitness);

            mockGetRandomGene.Verify(m => m.Invoke(),  Times.Exactly(2400));
        }
    }
}
