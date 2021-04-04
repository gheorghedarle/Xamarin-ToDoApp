using Prism.Navigation;
using System.Windows.Input;
using ToDoApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        public string Name { get; set; }

        public ICommand StartCommand { get; set; }

        public WelcomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        { 
            StartCommand = new Command(StartCommandHandler);
        }

        private async void StartCommandHandler()
        {
            Preferences.Set("Name", Name);
            await _navigationService.NavigateAsync("/TasksPage");
        }
    }
}
