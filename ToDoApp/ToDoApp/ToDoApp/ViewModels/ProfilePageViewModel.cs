using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class ProfilePageViewModel : 
        BaseViewModel,
        IInitialize
    {
        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand DarkModeToggleCommand { get; set; }
        public ICommand HideDoneToggleCommand { get; set; }

        #endregion

        #region Properties

        public bool IsDarkMode { get; set; }

        #endregion

        #region Constructors

        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            BackCommand = new Command(BackCommandHandler);
            DarkModeToggleCommand = new Command(DarkModeToggleCommandHandler);
            HideDoneToggleCommand = new Command(HideDoneToggleCommandHandler);
        }

        public void Initialize(INavigationParameters parameters)
        {
            IsDarkMode = Application.Current.UserAppTheme.Equals(OSAppTheme.Dark);
        }

        #endregion

        #region Command Handlers

        private void DarkModeToggleCommandHandler()
        {
            if (IsDarkMode)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("theme", "dark");
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("theme", "light");
            }
        }

        private void HideDoneToggleCommandHandler()
        { }

        #endregion
    }
}
