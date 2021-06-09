using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {

        #region Properties

        public string Type { get; set; }

        #endregion

        #region Commands

        public ICommand ChangeTypeCommand { get; set; }


        #endregion

        #region Constructors

        public AddPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            ChangeTypeCommand = new Command<string>(ChangeTypeCommandHandler);

            Type = "task";
        }

        #endregion

        #region Command Handlers

        private void ChangeTypeCommandHandler(string type)
        {
            Type = type;
        }

        #endregion
    }
}
