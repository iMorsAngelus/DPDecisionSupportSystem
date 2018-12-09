using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.DataAccessLayer;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.Command;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    /// Main view model, who contains all other.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        #region private fields

        private readonly IDataBaseProvider _dataBaseProvider;
        private IPageViewModel _currentPageViewModel;
        private ActionCommand _goHomeCommand;
        private ActionCommand _goInputFormCommand;
        private ActionCommand _goPairFormCommand;
        private InputViewModel _inputViewModel;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.        
        /// </summary>
        /// <param name="viewModelList"></param>
        /// <param name="dataBaseProvider"></param>
        public MainWindowViewModel(List<IPageViewModel> viewModelList, IDataBaseProvider dataBaseProvider)
        {
            _dataBaseProvider = dataBaseProvider;
            PageViewModels = viewModelList;

            CurrentPageViewModel = PageViewModels.FirstOrDefault();

            Mediator.Subscribe("GoHome", OnGoHome);
            Mediator.Subscribe("GoInputForm", OnGoInputForm);
            Mediator.Subscribe("GoPairForm", OnGoPairForm);
        }

        public List<IPageViewModel> PageViewModels { get; }

        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoHomeCommand => _goHomeCommand ?? (_goHomeCommand = new ActionCommand(param => { Mediator.Notify("GoHome", param); }));
        public ICommand GoInputFormCommand => _goInputFormCommand ?? (_goInputFormCommand = new ActionCommand(param => { Mediator.Notify("GoInputForm"); }));
        public ICommand GoPairFormCommand => _goPairFormCommand ?? (_goPairFormCommand = new ActionCommand(param => { Mediator.Notify("GoPairForm"); }));

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGoHome(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGoInputForm(object obj)
        {
            //var decisionTask = (PageViewModels[0] as TaskManagingViewModel)?.DecisionTasks[0];
            //_inputViewModel = new InputViewModel(decisionTask.Criterias, decisionTask.Alternatives);
            //ChangeViewModel(_inputViewModel);
        }

        private void OnGoPairForm(object obj)
        {
            //var decisionTask = (PageViewModels[0] as TaskManagingViewModel)?.DecisionTasks[0];
            //var pairMatrix = new List<PairMatrix<double>>{ new PairMatrix<double>(_inputViewModel.CriteriaCount, 1)};

            //foreach (var criteria in _inputViewModel.Criterias)
            //{
            //    pairMatrix.Add(new PairMatrix<double>(_inputViewModel.AlternativeCount, 1));
            //}

            //var pairForm = new PairMatrixViewModel(pairMatrix, decisionTask);
            //ChangeViewModel(pairForm);
        }
    }
}