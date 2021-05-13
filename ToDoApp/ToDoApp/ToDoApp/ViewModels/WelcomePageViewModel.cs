using Prism.Navigation;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        { 
            LoginCommand = new Command(LoginCommandHandler);
            RegisterCommand = new Command(RegisterCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void LoginCommandHandler()
        {
            Preferences.Set("Name", Email);

            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var token = await auth.LoginWithEmailAndPassword(Email, Password);

            if(token != string.Empty)
            {
                await _navigationService.NavigateAsync(nameof(TasksPage));
            }
            else
            {
                // display error
            }
        }

        private async void RegisterCommandHandler()
        {
            Preferences.Set("Name", Email);

            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var created = await auth.RegisterWithEmailAndPassword(Email, Password);

            if (created)
            {
                Debug.WriteLine("User Created");
            }
            else
            {
                // display error
            }
        }

        #endregion
    }
}
