using Prism.Navigation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel :
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IFirestoreRepository<TaskModel> _tasksRepository;
        private IFirestoreRepository<ListModel> _listsRepository;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region Properties

        public string Type { get; set; }
        public ObservableCollection<string> ItemList { get; set; }
        public ObservableCollection<Color> ColorList { get; set; }
        public ObservableCollection<ListModel> ProjectList { get; set; }
        public ObservableCollection<TaskModel> TaskList { get; set; }
        public TaskModel AddTask { get; set; }
        public ListModel AddList { get; set; }

        #endregion

        #region Commands

        public ICommand ChangeTypeCommand { get; set; }
        public ICommand CreateCommand { get; }

        #endregion

        #region Constructors

        public AddPageViewModel(
            INavigationService navigationService,
            IFirestoreRepository<TaskModel> tasksRepository,
            IFirestoreRepository<ListModel> listsRepository) : base(navigationService)
        {
            _tasksRepository = tasksRepository;
            _listsRepository = listsRepository;

            ChangeTypeCommand = new Command<string>(ChangeTypeCommandHandler);
            CreateCommand = ReactiveCommand.Create(CreateCommandHandler);

            ItemList = Constants.AddOptions;
            ColorList = Constants.ListColorList;

            Type = "task";
        }

        public async void Initialize(INavigationParameters parameters)
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var userId = auth.GetUserId();

            AddTask = new TaskModel();

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
            ProjectList = new ObservableCollection<ListModel>(listToAdd);
            AddTask = Constants.DefaultTask;
        }

        #endregion

        #region Command Handlers

        private void ChangeTypeCommandHandler(string type)
        {
            Type = type;
        }

        private async Task CreateCommandHandler()
        {
            try
            {
                var auth = DependencyService.Get<IFirebaseAuthentication>();
                var userId = auth.GetUserId();
                if(Type == "task")
                {
                    var model = new TaskModel()
                    {
                        archived = false,
                        list = AddTask.listObject.name,
                        task = AddTask.task,
                        userId = userId,
                        date = DateTime.Parse(AddTask.date).ToString("dd/MM/yyyy")
                    };
                    await _tasksRepository.Add(model);
                }
                else
                {
                    var model = new ListModel()
                    {
                        name = AddList.name,
                        color = AddList.color,
                        userId = userId
                    };
                    await _listsRepository.Add(model);
                }
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                //display error message
                Debug.Write(ex.Message);
            }
        }

        #endregion
    }
}
