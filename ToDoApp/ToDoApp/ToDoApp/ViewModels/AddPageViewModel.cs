using Prism.Navigation;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoApp.Helpers;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel :
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IRegionManager _regionManager { get; }

        #endregion

        #region Properties

        public string Type { get; set; }
        public ObservableCollection<string> ItemList { get; set; }

        #endregion

        #region Commands

        public ICommand ChangeTypeCommand { get; set; }
        public ICommand BackCommand { get; set; }

        #endregion

        #region Constructors

        public AddPageViewModel(
            INavigationService navigationService, 
            IRegionManager regionManager) : base(navigationService)
        {
            _regionManager = regionManager;

            BackCommand = new Command(BackCommandHandler);
            ChangeTypeCommand = new Command<string>(ChangeTypeCommandHandler);

            ItemList = Constants.AddOptions;
        }

        public void Initialize(INavigationParameters parameters)
        {
            Type = "task";

            _regionManager.RequestNavigate("AddTaskRegion", "AddTaskTemplate");
            _regionManager.RequestNavigate("AddListRegion", "AddListTemplate");
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
