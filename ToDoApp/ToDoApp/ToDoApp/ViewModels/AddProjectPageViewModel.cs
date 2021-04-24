using Prism.Navigation;

namespace ToDoApp.ViewModels
{
    public class AddProjectPageViewModel : BaseViewModel
    {
        public AddProjectPageViewModel(
            INavigationService navigationService) : base(navigationService)
        { }
    }
}
