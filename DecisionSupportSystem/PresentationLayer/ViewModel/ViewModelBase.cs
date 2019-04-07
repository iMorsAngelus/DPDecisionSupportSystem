using System;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    /// <summary>
    /// Base class for all view models.
    /// </summary>
    public abstract class ViewModelBase : NotifyPropertyChanged, IPageViewModel
    {
        /// <summary>
        /// AlternativeName of view model.
        /// </summary>
        public String DisplayName { get; set; }

        public abstract void UpdateDataOnPage();
    }
}