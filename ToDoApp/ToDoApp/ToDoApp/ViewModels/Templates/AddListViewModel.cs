using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Helpers;
using ToDoApp.Models;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates
{
    public class AddListViewModel : BaseViewModel
    {
        public Task Initialization { get; private set; }

        public ListModel AddList { get; set; }
        public ObservableCollection<ColorModel> ColorList { get; set; }

        public ICommand CreateCommand { get; set; }

        public AddListViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            CreateCommand = new Command(CreateCommandHandler);

            Initialization = Initialize();
        }
        public Task Initialize()
        {
            ColorList = new ObservableCollection<ColorModel>(Constants.ListColorList);
            AddList = Constants.DefaultList;

            return Task.CompletedTask;
        }

        private void CreateCommandHandler()
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
    }
}
