using System.Collections.Generic;
using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.Common;
using DecisionSupportSystem.DataAccessLayer.DbModels;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class ResultViewModel : ViewModelBase, IPageViewModel
    {
        private readonly IDataBaseProvider _provider;
        private readonly PriorityVectorSearcher _priorityVectorSearcher;

        public ResultViewModel(IDataBaseProvider provider, PriorityVectorSearcher priorityVectorSearcher)
        {
            _provider = provider;
            _priorityVectorSearcher = priorityVectorSearcher;
            Search();
        }

        private void Search()
        {
            var pairMatrices = BoxingExtension.Unboxing(_provider.CurrentTask.PairMatrices) as List<PairMatrix<double>>;
            var results = new List<double[]>();
            pairMatrices.ForEach(matrix => results.Add(_priorityVectorSearcher.SearchAsync(matrix.Size, matrix).Result));
        }
    }
}