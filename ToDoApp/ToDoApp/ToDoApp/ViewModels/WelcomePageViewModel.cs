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

        public WelcomePageViewModel()
        {
            StartCommand = new Command(StartCommandHandler);
        }

        private async void StartCommandHandler()
        {
            Preferences.Set("Name", Name);
            await Application.Current.MainPage.Navigation.PushAsync(new TasksPage());
        }
    }
}
