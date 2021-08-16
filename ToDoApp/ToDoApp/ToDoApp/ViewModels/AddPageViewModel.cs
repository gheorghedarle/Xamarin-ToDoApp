using Prism.Navigation;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        private async Task CreateCommandHandler()
        {
            //try
            //{
            //    var auth = DependencyService.Get<IFirebaseAuthentication>();
            //    var userId = auth.GetUserId();
            //    if(Type == "task")
            //    {
            //        var model = new TaskModel()
            //        {
            //            archived = false,
            //            list = AddTask.listObject.name,
            //            task = AddTask.task,
            //            userId = userId,
            //            date = DateTime.Parse(AddTask.date).ToString("dd/MM/yyyy")
            //        };
            //        await _tasksRepository.Add(model);
            //    }
            //    else
            //    {
            //        var model = new ListModel()
            //        {
            //            name = AddList.name,
            //            color = AddList.color,
            //            userId = userId
            //        };
            //        await _listsRepository.Add(model);
            //    }
            //    await _navigationService.GoBackAsync();
            //}
            //catch (Exception ex)
            //{
            //    //display error message
            //    Debug.Write(ex.Message);
            //}
        }

        #endregion
    }
}
