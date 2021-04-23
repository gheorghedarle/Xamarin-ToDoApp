using Prism.Navigation;

namespace ToDoApp.ViewModels
{
    public class AddTaskPageViewModel: BaseViewModel
    {
        public AddTaskPageViewModel(
            INavigationService navigationService) : base(navigationService)
        { }
    }
}
