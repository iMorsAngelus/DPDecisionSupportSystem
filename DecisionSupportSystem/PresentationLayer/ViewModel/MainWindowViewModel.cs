using System;
using DecisionSupportSystem.PresentationLayer.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DecisionSupportSystem.DataAccessLayer.DbModels;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    /// Main view model, who contains all other.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        private readonly IDataBaseProvider _dataProvider;

        #region private fields
        private IPageViewModel _currentPageViewModel;
        private Predicate<object> _canGoInputForm;
        private Predicate<object> _canGoPairForm;
        private Predicate<object> _canGoResultForm;
        private ActionCommand _goHomeCommand;
        private ActionCommand _goInputFormCommand;
        private ActionCommand _goPairFormCommand;
        private ActionCommand _goResultFormCommand;
        private IPageViewModel FindModel(string name) => PageViewModels.Find(vm => vm.DisplayName.Equals(name));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.        
        /// </summary>
        /// <param name="viewModelList"></param>
        /// <param name="dataProvider"></param>
        public MainWindowViewModel(List<IPageViewModel> viewModelList, IDataBaseProvider dataProvider)
        {
            _dataProvider = dataProvider;
            PageViewModels = viewModelList;

            UpdateDataOnPage();
            InitializePredicates();
            InitializeCommands();
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

        public ICommand GoHomeCommand => _goHomeCommand;
        public ICommand GoInputFormCommand => _goInputFormCommand;
        public ICommand GoPairFormCommand => _goPairFormCommand;
        public ICommand GoResultFormCommand => _goResultFormCommand;

        public sealed override void UpdateDataOnPage()
        {
            CurrentPageViewModel = PageViewModels.FirstOrDefault();

            Mediator.Subscribe("CanExecuteChanged", OnCommandCanExecuteChanged);
            Mediator.Subscribe("GoHome", OnGoHome);
            Mediator.Subscribe("GoInputForm", OnGoInputForm);
            Mediator.Subscribe("GoPairForm", OnGoPairForm);
            Mediator.Subscribe("GoResultForm", OnGoResultForm);
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            viewModel.UpdateDataOnPage();

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGoHome(object obj)
        {
            ChangeViewModel(FindModel("TaskManagingViewModel"));
            _goInputFormCommand.NotifyCanExecuteChanged();
        }

        private void OnGoInputForm(object obj)
        {
            ChangeViewModel(FindModel("InputViewModel"));
        }

        private void OnGoPairForm(object obj)
        {
            ChangeViewModel(FindModel("PairMatrixViewModel"));
        }

        private void OnGoResultForm(object obj)
        {
            ChangeViewModel(FindModel("ResultViewModel"));
        }

        private void OnCommandCanExecuteChanged(object obj)
        {
            _goHomeCommand.NotifyCanExecuteChanged();
            _goInputFormCommand.NotifyCanExecuteChanged();
            _goPairFormCommand.NotifyCanExecuteChanged();
            _goResultFormCommand.NotifyCanExecuteChanged();
        }

        private void InitializePredicates()
        {
            _canGoInputForm = o => _dataProvider.CurrentTask != null;

            _canGoPairForm = o => _canGoInputForm(o) &&
                                  _dataProvider.CurrentTask.Alternatives != null &&
                                  _dataProvider.CurrentTask.Alternatives.Count > 0 &&
                                  _dataProvider.CurrentTask.Criterias != null &&
                                  _dataProvider.CurrentTask.Criterias.Count > 0;

            _canGoResultForm = o => _canGoPairForm(o) &&
                                    _dataProvider.CurrentTask.PairMatrices != null;
        }

        private void InitializeCommands()
        {
            _goHomeCommand = new ActionCommand(param =>
            {
                Mediator.Notify("GoHome", param);
            });
            _goInputFormCommand = new ActionCommand(param =>
            {
                Mediator.Notify("GoInputForm");
            }, _canGoInputForm);
            _goPairFormCommand = new ActionCommand(param =>
            {
                Mediator.Notify("GoPairForm");
            }, _canGoPairForm);
            _goResultFormCommand = new ActionCommand(param =>
            {
                Mediator.Notify("GoResultForm");
            }, _canGoResultForm);
        }
    }
}