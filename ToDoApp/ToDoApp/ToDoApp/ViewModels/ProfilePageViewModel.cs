using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand DarkModeToggleCommand { get; set; }
        public ICommand HideDoneToggleCommand { get; set; }

        #endregion

        #region Constructors

        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            BackCommand = new Command(BackCommandHandler);
            DarkModeToggleCommand = new Command(DarkModeToggleCommandHandler);
            HideDoneToggleCommand = new Command(HideDoneToggleCommandHandler);
        }

        #endregion

        #region Command Handlers

        private void DarkModeToggleCommandHandler()
        { }

        private void HideDoneToggleCommandHandler()
        { }

        #endregion
    }
}
