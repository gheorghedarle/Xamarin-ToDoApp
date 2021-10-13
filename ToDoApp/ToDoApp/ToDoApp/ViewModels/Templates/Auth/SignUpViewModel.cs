using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates.Auth
{
    public class SignUpViewModel : BaseRegionViewModel
    {
        #region Properties
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        #endregion

        #region Commands

        public ICommand SignUpCommand { get; set; }

        #endregion

        #region Constructors

        public SignUpViewModel(
            INavigationService navigationService) : base(navigationService)
        {
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
                    var created = await auth.RegisterWithEmailAndPassword(Username, Email, Password);

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
                // display error
            }
            finally
            {
                MainState = LayoutState.None;
            }
        }

        #endregion

        #region Private Functionality

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
