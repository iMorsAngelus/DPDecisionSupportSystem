using System;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    /// <summary>
    /// Base class for all view models.
    /// </summary>
    public class ViewModelBase : NotifyPropertyChanged
    {

        /// <summary>
        /// AlternativeName of view model.
        /// </summary>
        public String DisplayName { get; set; }
    }
}