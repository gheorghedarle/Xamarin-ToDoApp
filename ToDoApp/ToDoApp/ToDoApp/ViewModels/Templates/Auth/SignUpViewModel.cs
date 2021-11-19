using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers.Validations;
using ToDoApp.Helpers.Validations.Rules;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates.Auth
{
    public class SignUpViewModel : BaseRegionViewModel
    {
        #region Properties
        public ValidatableObject<string> Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        #endregion

        #region Commands

        public ICommand SignUpCommand { get; set; }
        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public SignUpViewModel(
            INavigationService navigationService) : base(navigationService)
        {

            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "username": Username.Validate(); break;
            }
        }

        #endregion

        #region Command Handlers

        private async void SignUpCommandHandler()
        {
            try
            {
                MainState = LayoutState.Loading;
                if (ValidateSignUpData())
                {
                    var auth = DependencyService.Get<IFirebaseAuthentication>();
                    var created = await auth.RegisterWithEmailAndPassword(Username.Value, Email, Password);

                    if (created)
                    {
                        ClearAuthData();
                        //CurrentAuthScreen = "Login";
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
            Username = new ValidatableObject<string>();

            Username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
        }

        private bool ValidateSignUpData()
        {
            if (Username.IsValid ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword) ||
                !Password.Equals(ConfirmPassword))
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Username.Value = Email = Password = ConfirmPassword = string.Empty;
        }

        #endregion
    }
}
