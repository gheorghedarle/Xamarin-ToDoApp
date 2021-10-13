using Prism.Navigation;
using Prism.Regions.Navigation;
using System;
using System.ComponentModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace ToDoApp.ViewModels
{
    public class BaseRegionViewModel: IRegionAware
    {
        #region Private & Protected

        protected INavigationService _navigationService { get; set; }

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public LayoutState MainState { get; set; }

        #endregion

        #region Constructor

        public BaseRegionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        #region Region Navigation Handlers

        public void OnNavigatedTo(INavigationContext navigationContext) { }

        public bool IsNavigationTarget(INavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(INavigationContext navigationContext) { }

        #endregion
    }
}
