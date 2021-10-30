using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates.Auth
{
    public class LoginViewModel : BaseRegionViewModel
    {
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructors

        public LoginViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            LoginCommand = new Command(LoginCommandHandler);
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
                        Preferences.Set("taskFilterByList", "All lists");
                        await _navigationService.NavigateAsync($"/{nameof(TasksPage)}");
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

        private bool ValidateLoginData()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password))
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Email = Password = string.Empty;
        }

        #endregion
    }
}
