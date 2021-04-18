using Prism.Navigation;
using System.Windows.Input;
using ToDoApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Commands

        public ICommand StartCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        { 
            StartCommand = new Command(StartCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void StartCommandHandler()
        {
            Preferences.Set("Name", Name);
            await _navigationService.NavigateAsync(nameof(TasksPage));
        }

        #endregion
    }
}
