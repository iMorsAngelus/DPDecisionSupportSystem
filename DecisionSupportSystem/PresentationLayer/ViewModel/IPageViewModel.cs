using System;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    public interface IPageViewModel
    {
        String DisplayName { get; }

        void UpdateDataOnPage();
    }
}