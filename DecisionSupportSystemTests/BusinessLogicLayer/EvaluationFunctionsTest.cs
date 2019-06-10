using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;
using Moq;
using NUnit.Framework;
using System;

namespace DecisionSupportSystemTests.BusinessLogicLayer
{
    [TestFixture]
    class EvaluationFunctionsTest
    {
        private Mock<Random> mockRandom;
        private Mock<IPairMatrix<double>> mockPairMatrix;

        private EvaluationFunctions unit;

        [SetUp]
        public void SetUp()
        {
            //Initialize
            mockRandom = new Mock<Random>();
            mockPairMatrix = new Mock<IPairMatrix<double>>();

            //Create unit to test
            unit = new EvaluationFunctions(3, mockPairMatrix.Object, mockRandom.Object);
        }

        [Test]
        public void GetRandomGene_Test()
        {
            //GIVEN
            mockRandom.Setup(m => m.Next(0, 2)).Returns(1);

            //WHEN
            var result = unit.GetRandomGene();

            //THEN
            Assert.AreEqual(result, 1);
            mockRandom.Verify(m => m.Next(0, 2), Times.Once);
        }

        [Test]
        public void EvaluateFitness_ExpectException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => unit.EvaluateFitness(null));
        }

        [Test]
        public void EvaluateFitness_Test()
        {
            //GIVEN
            var rnd = new Random();
            mockRandom.Setup(m => m.Next(0, 2)).Returns(() => {
                return rnd.Next(0, 2);
            });

            mockPairMatrix.Setup(m => m.Matrix).Returns(new FuzzyNumber<double>[3,3] {

                {
                    new FuzzyNumber<double>(1),
                    new FuzzyNumber<double>(new double[] {1, 2, 3}),
                    new FuzzyNumber<double>(new double[] {-3, -2,-1})
                },
                {
                    new FuzzyNumber<double>(new double[] {1, 2, 3}),
                    new FuzzyNumber<double>(1),
                    new FuzzyNumber<double>(new double[] {-3, -2,-1}) },
                {
                    new FuzzyNumber<double>(new double[] {1, 2, 3}),
                    new FuzzyNumber<double>(new double[] {-3, -2,-1}),
                    new FuzzyNumber<double>(1)
                }
            });

            var chromosome = new Chromosome<byte>(24, new Random(), unit.GetRandomGene, unit.EvaluateFitness);

            //WHEN
            var result = unit.EvaluateFitness(chromosome);

            //THEN
            Assert.IsFalse(double.IsInfinity(result));

            mockPairMatrix.Verify(m => m.Matrix, Times.Between(27, 30, Range.Inclusive));
        }
    }
}
