using System;
using System.Collections.Generic;
using System.Linq;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    [Serializable]
    public class PairMatrix<T> : IPairMatrix<T>
    {
        public FuzzyNumber<T>[,] Matrix { get; }

        public readonly int Size;
        private readonly T _defaultValue;
        private readonly List<TwoDimensionalArrayIndexs> _upperTriangleIndexes;
        private readonly List<TwoDimensionalArrayIndexs> _lowerTriangleIndexes;

        public PairMatrix(int size, T defaultValue)
        {
            Size = size;
            _defaultValue = defaultValue;
            _lowerTriangleIndexes = new List<TwoDimensionalArrayIndexs>();
            _upperTriangleIndexes = new List<TwoDimensionalArrayIndexs>();
            Matrix = new FuzzyNumber<T>[Size, Size];
            PopulateDiagonal();
            SetUpperTriangleIndexes();
        }

        public void PopulateLowerTriangle()
        {
            if (!IsUpperTrianglePopulated) throw new ArgumentNullException($"Upper triangle of matrix isn't populated");

            //find lower triangle indexes
            for (var i = 1; i < Size; i++)
            {
                var indexes = Enumerable.Range(0, i)
                    .Select(generatedNum => new TwoDimensionalArrayIndexs(i, generatedNum));
                _lowerTriangleIndexes.AddRange(indexes);
            }

            //create divided value
            var dividedValue = new FuzzyNumber<T>(_defaultValue);

            //Set indexes
            _lowerTriangleIndexes.ForEach(index =>
            {
                Matrix[index.Column, index.Row].RevertIfNegative();
                Matrix[index.Row, index.Column] = dividedValue / Matrix[index.Column, index.Row];
            });
        }

        public TwoDimensionalArrayIndexs GetUnpopulatedIndex => _upperTriangleIndexes.FirstOrDefault();

        public void SetFirstUnpopulatedElementInUpperTriangle(FuzzyNumber<T> value)
        {
            var index = _upperTriangleIndexes.FirstOrDefault();

            Matrix[index.Row, index.Column] = value;

            _upperTriangleIndexes.Remove(index);
        }

        public override string ToString() =>
            String.Join("\r\n\r\n",
                Enumerable.Range(0, Size).Select(rowIndex =>
                    String.Join("  ",
                        Enumerable.Range(0, Size).Select(columnIndex => Matrix[rowIndex, columnIndex]))));

        private bool IsUpperTrianglePopulated =>
            _upperTriangleIndexes.All(index => Matrix[index.Row, index.Column] != null);

        private void SetUpperTriangleIndexes()
        {
            for (var i = 0; i < Size - 1; i++)
            {
                var indexes = Enumerable.Range(i + 1, Size - i - 1)
                    .Select(generatedNum => new TwoDimensionalArrayIndexs(i, generatedNum));
                _upperTriangleIndexes.AddRange(indexes);
            }
        }

        private void PopulateDiagonal()
        {
            for (var i = 0; i < Size; i++)
            {
                Matrix[i, i] = new FuzzyNumber<T>(_defaultValue);
            }
        }
    }
}