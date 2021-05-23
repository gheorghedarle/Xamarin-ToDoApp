using Prism.Navigation;
using System.Collections.ObjectModel;
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

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ObservableCollection<string> AuthScreenList { get; set; }
        public string CurrentAuthScreen { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand SwitchToLoginCommand { get; set; }
        public ICommand SwitchToSignUpCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        { 
            LoginCommand = new Command(LoginCommandHandler);
            RegisterCommand = new Command(RegisterCommandHandler);
            SwitchToLoginCommand = new Command(SwitchToLoginCommandHandler);
            SwitchToSignUpCommand = new Command(SwitchToSignUpCommandHandler);

            AuthScreenList = new ObservableCollection<string>() { "Login", "SignUp" };
            CurrentAuthScreen = "Login";
        }

        #endregion

        #region Command Handlers

        private async void LoginCommandHandler()
        {
            Preferences.Set("Name", Email);

            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var user = await auth.LoginWithEmailAndPassword(Email, Password);

            if(user != null)
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
            var created = await auth.RegisterWithEmailAndPassword(Username, Email, Password);

            if (created)
            {
                Debug.WriteLine("User Created");
            }
            else
            {
                // display error
            }
        }


        private void SwitchToLoginCommandHandler()
        {
            CurrentAuthScreen = "Login";
        }


        private void SwitchToSignUpCommandHandler()
        {
            CurrentAuthScreen = "SignUp";
        }

        #endregion
    }
}
