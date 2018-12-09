using DecisionSupportSystem.DataAccessLayer;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;

namespace DecisionSupportSystem.BusinessLogicLayer
{
    public interface IPairMatrix<T>
    {
        FuzzyNumber<T>[,] Matrix { get; }
        TwoDimensionalArrayIndexs GetUnpopulatedIndex { get; }

        void PopulateLowerTriangle();

        void SetFirstUnpopulatedElementInUpperTriangle(FuzzyNumber<T> value);

        string ToString();
    }
}