using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoApp.Helpers;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {
        #region Properties

        public string Type { get; set; }
        public ObservableCollection<string> ItemList { get; set; }
        public ObservableCollection<Color> ColorList { get; set; }

        #endregion

        #region Commands

        public ICommand ChangeTypeCommand { get; set; }


        #endregion

        #region Constructors

        public AddPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            ChangeTypeCommand = new Command<string>(ChangeTypeCommandHandler);

            ItemList = Constants.AddOptions;
            ColorList = Constants.ListColorList;

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
