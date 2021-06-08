using Prism.Navigation;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {
        public string Type { get; set; }

        public AddPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Type = "task";
        }
    }
}
