using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers.Validations;
using ToDoApp.Helpers.Validations.Rules;
using ToDoApp.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates.Auth
{
    public class LoginViewModel : BaseRegionViewModel
    {
        #region Properties

        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand ValidateCommand { get; set; }


        #endregion

        #region Constructors

        public LoginViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            LoginCommand = new Command(LoginCommandHandler);

            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "email": Email.Validate(); break;
                case "password": Password.Validate(); break;
            }
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
                    var user = await auth.LoginWithEmailAndPassword(Email.Value, Password.Value);

                    if (user != null)
                    {
                        ClearAuthData();
                        Preferences.Set("taskFilterByList", "All lists");
                        await _navigationService.NavigateAsync($"/{nameof(TasksPage)}");
                    }
                    else
                    {
                        // display error message
                    }
                }
            }
            catch (Exception ex)
            {
                // display error message
                Debug.WriteLine(ex);
            }
            finally
            {
                MainState = LayoutState.None;
            }
        }

        #endregion

        #region Private Functionality

        private void AddValidations()
        {
            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A email is required." });
            Email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email format is not correct" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private bool ValidateLoginData()
        {
            if (Email.IsValid == false ||
                Password.IsValid == false)
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Email.Value = Password.Value = string.Empty;
        }

        #endregion
    }
}
