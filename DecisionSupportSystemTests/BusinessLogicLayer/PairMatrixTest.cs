using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;
using NUnit.Framework;
using System;

namespace DecisionSupportSystemTests.BusinessLogicLayer
{
    [TestFixture]
    class PairMatrixTest
    {
        private PairMatrix<double> unit;

        [SetUp]
        public void SetUp()
        {
            unit = new PairMatrix<double>(3, 1);
        }

        [Test]
        public void GetUnpopulatedIndex()
        {
            //WHEN
            var index = unit.GetUnpopulatedIndex;

            //THEN
            Assert.AreEqual(index.Row, 0);
            Assert.AreEqual(index.Column, 1);
        }

        [Test]
        public void SetFirstUnpopulatedIndex()
        {
            //GIVEN
            var fuzzyNumber = new FuzzyNumber<double>(new double[] { 1, 2, 3 });
            var indexPrev = unit.GetUnpopulatedIndex;

            //WHEN
            unit.SetFirstUnpopulatedElementInUpperTriangle(fuzzyNumber);
            var indexNext = unit.GetUnpopulatedIndex;

            //THEN
            Assert.AreNotEqual(indexPrev, indexNext);
            Assert.AreEqual(unit.Matrix[indexPrev.Row, indexPrev.Column], fuzzyNumber);
            Assert.AreEqual(indexNext.Row, 0);
            Assert.AreEqual(indexNext.Column, 2);
        }

        [Test]
        public void PopulateLowerTriangle_ExceptionExpected()
        {
            //WHEN
            Assert.Throws<ArgumentNullException>(() => unit.PopulateLowerTriangle());
        }

        [Test]
        public void PopulateLowerTriangle()
        {
            //GIVEN
            var fuzzyNumber = new FuzzyNumber<double>(new double[] { 1, 2, 3 });
            var fuzzyNumber2 = new FuzzyNumber<double>(new double[] { -3, -2, -1 });
            var fuzzyNumber3 = new FuzzyNumber<double>(new double[] { 2, 3, 4 });

            //WHEN
            unit.SetFirstUnpopulatedElementInUpperTriangle(fuzzyNumber);
            unit.SetFirstUnpopulatedElementInUpperTriangle(fuzzyNumber2);
            unit.SetFirstUnpopulatedElementInUpperTriangle(fuzzyNumber3);
            unit.PopulateLowerTriangle();

            //THEN
            //1 0, 2 0, 2 1
            Assert.That(unit.Matrix[1, 0].Numbers != null);
            Assert.That(unit.Matrix[2, 0].Numbers != null);
            Assert.That(unit.Matrix[2, 1].Numbers != null);
        }
    }
}
