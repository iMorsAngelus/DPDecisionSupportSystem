using DecisionSupportSystem.PresentationLayer.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    /// Main view model, who contains all other.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        #region private fields
        private IPageViewModel _currentPageViewModel;
        private ActionCommand _goHomeCommand;
        private ActionCommand _goInputFormCommand;
        private ActionCommand _goPairFormCommand;
        private ActionCommand _goResultFormCommand;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.        
        /// </summary>
        /// <param name="viewModelList"></param>
        public MainWindowViewModel(List<IPageViewModel> viewModelList)
        {
            PageViewModels = viewModelList;

            UpdateDataOnPage();
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
        public ICommand GoResultFormCommand => _goResultFormCommand ?? (_goResultFormCommand = new ActionCommand(param => { Mediator.Notify("GoResultForm"); }));

        public sealed override void UpdateDataOnPage()
        {
            CurrentPageViewModel = PageViewModels.FirstOrDefault();

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
            ChangeViewModel(PageViewModels.Find(vm => vm.DisplayName.Equals("TaskManagingViewModel")));
        }

        private void OnGoInputForm(object obj)
        {
            ChangeViewModel(PageViewModels.Find(vm => vm.DisplayName.Equals("InputViewModel")));
        }

        private void OnGoPairForm(object obj)
        {
            ChangeViewModel(PageViewModels.Find(vm => vm.DisplayName.Equals("PairMatrixViewModel")));
        }

        private void OnGoResultForm(object obj)
        {
            ChangeViewModel(PageViewModels.Find(vm => vm.DisplayName.Equals("ResultViewModel")));
        }
    }
}