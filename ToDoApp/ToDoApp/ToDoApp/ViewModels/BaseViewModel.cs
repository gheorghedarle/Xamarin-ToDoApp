using Prism.AppModel;
using Prism.Navigation;
using System.ComponentModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace ToDoApp.ViewModels
{
    public class BaseViewModel: 
        INavigationAware, 
        IPageLifecycleAware, 
        INotifyPropertyChanged, 
        IDestructible
    {
        #region Private & Protected

        protected INavigationService _navigationService { get; set; }

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public LayoutState MainState { get; set; }
        public bool HasNoInternetConnection { get; set; }

        #endregion

        #region Constructor

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Connectivity.ConnectivityChanged += ConnectivityChanged;
            HasNoInternetConnection = !Connectivity.NetworkAccess.Equals(NetworkAccess.Internet);
        }

        #endregion

        #region

        protected async void BackCommandHandler()
        {
            await _navigationService.GoBackAsync();
        }

        #endregion

        #region Internet Connection

        private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            HasNoInternetConnection = !e.NetworkAccess.Equals(NetworkAccess.Internet);
        }

        #endregion

        #region INavigationAware

        public virtual void OnNavigatingTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        #endregion

        #region IPageLifecycleAware

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        #endregion

        #region IDestructible

        public virtual void Destroy() { }

        #endregion
    }
}
