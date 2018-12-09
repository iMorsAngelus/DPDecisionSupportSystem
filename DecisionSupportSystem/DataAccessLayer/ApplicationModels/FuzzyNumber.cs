using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    public class FuzzyNumber<T>
    {
        public List<T> Numbers { get; } = new List<T>();
        private int Size { get; } = 3;
        private readonly int _maxNumberSize = 5;

        public FuzzyNumber(IEnumerable<T> numbers)
        {
            Numbers.AddRange(numbers);
        }

        public FuzzyNumber(T number)
        {
            Numbers.AddRange(PopulateWithSameNumbers(number));
        }

        public override string ToString()
        {
            var output = "";

            Numbers.ForEach(num =>
            {
                output += num.ToString();
                output += new string(' ', _maxNumberSize + 2 - num.ToString().Length);

            });

            return output;
        }

        public static FuzzyNumber<T> operator /(FuzzyNumber<T> a, FuzzyNumber<T> b) =>
            new FuzzyNumber<T>(new[] { DivideFuzzyNumbers(a, b, 0), DivideFuzzyNumbers(a, b, 1), DivideFuzzyNumbers(a, b, 2) });

        private static T DivideFuzzyNumbers(FuzzyNumber<T> a, FuzzyNumber<T> b, int index)
        {
            return Math.Round((dynamic)a.Numbers[index] / (dynamic)b.Numbers[index], int.Parse(ConfigurationManager.AppSettings["CalculationAccuracy"]),
                MidpointRounding.AwayFromZero);
        }

        public static FuzzyNumber<T> operator *(FuzzyNumber<T> a, FuzzyNumber<T> b) =>
            new FuzzyNumber<T>(new[] { MultiplicationFuzzyNumbers(a, b, 0), MultiplicationFuzzyNumbers(a, b, 1), MultiplicationFuzzyNumbers(a, b, 2) });

        private static T MultiplicationFuzzyNumbers
            (FuzzyNumber<T> a, FuzzyNumber<T> b, int index)
        {
            return Math.Round((dynamic)a.Numbers[index] * (dynamic)b.Numbers[index], int.Parse(ConfigurationManager.AppSettings["CalculationAccuracy"]),
                MidpointRounding.AwayFromZero);
        }

        private IEnumerable<T> PopulateWithSameNumbers(T value)
        {
            return Enumerable.Repeat(value, Size);
        }
    }
}