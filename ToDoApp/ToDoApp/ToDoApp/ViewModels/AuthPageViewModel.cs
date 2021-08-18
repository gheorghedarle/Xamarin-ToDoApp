using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AuthPageViewModel : BaseViewModel
    {
        #region Properties

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public ObservableCollection<string> AuthScreenList { get; set; }
        public string CurrentAuthScreen { get; set; }

        #endregion

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SwitchToLoginCommand { get; set; }
        public ICommand SwitchToSignUpCommand { get; set; }

        #endregion

        #region Constructors

        public AuthPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            BackCommand = new Command(BackCommandHandler);
            LoginCommand = new Command(LoginCommandHandler);
            SignUpCommand = new Command(SignUpCommandHandler);
            SwitchToLoginCommand = new Command(SwitchToLoginCommandHandler);
            SwitchToSignUpCommand = new Command(SwitchToSignUpCommandHandler);

            AuthScreenList = new ObservableCollection<string>() { "Login", "SignUp" };
            CurrentAuthScreen = "Login";
        }

        #endregion

        #region Command Handlers

        private async void LoginCommandHandler()
        {
            try
            {
                MainState = LayoutState.Loading;
                if (ValidateLoginData())
                {
                    var auth = DependencyService.Get<IFirebaseAuthentication>();
                    var user = await auth.LoginWithEmailAndPassword(Email, Password);

                    if (user != null)
                    {
                        ClearAuthData();
                        await _navigationService.NavigateAsync(nameof(TasksPage));
                    }
                    else
                    {
                        // display error
                    }
                }
            }
            catch (Exception ex)
            {
                // display error
            }
            finally
            {
                MainState = LayoutState.None;
            }
        }

        private async void SignUpCommandHandler()
        {
            try
            {
                MainState = LayoutState.Loading;
                if (ValidateSignUpData())
                {
                    var auth = DependencyService.Get<IFirebaseAuthentication>();
                    var created = await auth.RegisterWithEmailAndPassword(Username, Email, Password);

                    if (created)
                    {
                        ClearAuthData();
                        CurrentAuthScreen = "Login";
                        Debug.WriteLine("User Created");
                    }
                    else
                    {
                        // display error
                    }
                }
            }
            catch (Exception ex)
            {
                // display error
            }
            finally
            {
                MainState = LayoutState.None;
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

        #region Private Functionality

        private bool ValidateLoginData()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password))
                return false;
            return true;
        }

        private bool ValidateSignUpData()
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword) ||
                !Password.Equals(ConfirmPassword))
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Username = Email = Password = ConfirmPassword = string.Empty;
        }

        #endregion
    }
}
