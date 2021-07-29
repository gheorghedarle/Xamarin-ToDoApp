using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class MorePageViewModel : BaseViewModel
    {
        #region Commands

        public ICommand BackCommand { get; set; }

        #endregion

        public MorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            BackCommand = new Command(BackCommandHandler);
        }
    }
}
