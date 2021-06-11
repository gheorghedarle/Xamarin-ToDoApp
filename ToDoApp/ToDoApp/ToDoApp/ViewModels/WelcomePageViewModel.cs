using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        #region Commands

        public ICommand GetStartedCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        { 
            GetStartedCommand = new Command(GetStartedCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void GetStartedCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(AuthPage));
        }

        #endregion
    }
}
