using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates
{
    public class AddTaskViewModel : BaseViewModel
    {
        private IFirestoreRepository<ListModel> _listsRepository;

        public Task Initialization { get; private set; }

        public ObservableCollection<ListModel> ProjectList { get; set; }

        public TaskModel AddTask { get; set; }

        public ICommand CreateCommand { get; set; }

        public AddTaskViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listsRepository): base(navigationService)
        {
            _listsRepository = listsRepository;

            CreateCommand = new Command(CreateCommandHandler);

            Initialization = Initialize();
        }

        public async Task Initialize()
        {
            var projectList = await GetProjectList();
            ProjectList = new ObservableCollection<ListModel>(projectList);

            AddTask = Constants.DefaultTask;
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

        private async Task<List<ListModel>> GetProjectList()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var userId = auth.GetUserId();

            var querySnapshot = await _listsRepository.GetAll(userId).GetAsync();
            var list = querySnapshot.ToObjects<ListModel>();
            var listToAdd = new List<ListModel>();
            if (list.Count() > 0)
            {
                listToAdd = list.ToList();
                listToAdd.Insert(0, Constants.InboxList);
            }
            else
            {
                listToAdd.Add(Constants.InboxList);
            }
            return listToAdd;
        }
    }
}
