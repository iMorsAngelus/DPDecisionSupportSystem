using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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
        private InputViewModel _inputViewModel;
        private IPageViewModel _currentPageViewModel;
        private ActionCommand _goHomeCommand;
        private ActionCommand _goInputFormCommand;
        private ActionCommand _goPairFormCommand;

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
            _inputViewModel = new InputViewModel(_dataBaseProvider);
            ChangeViewModel(_inputViewModel);
        }

        private void OnGoPairForm(object obj)
        {
            var pairForm = new PairMatrixViewModel(_dataBaseProvider);
            ChangeViewModel(pairForm);
        }
    }
}