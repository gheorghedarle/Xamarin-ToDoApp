using Prism.Navigation;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {
        public AddPageViewModel(
            INavigationService navigationService) : base(navigationService)
        { }
    }
}
