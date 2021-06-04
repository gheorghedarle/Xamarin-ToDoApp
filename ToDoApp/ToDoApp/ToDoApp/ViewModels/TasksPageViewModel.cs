using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Services.DateService;
using ToDoApp.Views;
using ToDoApp.Views.Dialogs;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel : BaseViewModel
    {
        #region Private & Protected

        private IDateService _dateService;
        private IFirestoreRepository<TaskModel> _tasksRepository;
        private IDialogService _dialogService;

        #endregion

        #region Properties

        public ObservableCollection<DayModel> DaysList { get; set; }
        public ObservableCollection<TaskModel> TaskList { get; set; }

        public LayoutState TaskListState {get;set;}

        public string Name { get; set; }
        public WeekModel Week { get; set; }

        #endregion

        #region Commands

        public ICommand CheckTaskCommand { get; set; }
        public ICommand DayCommand { get; set; }
        public ICommand PreviousWeekCommand { get; set; }
        public ICommand NextWeekCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand ItemDragged { get; }
        public ICommand ItemDraggedOver { get; }
        public ICommand ItemDragLeave { get; }
        public ICommand ItemDropped { get; }

        #endregion

        #region Constructors

        public TasksPageViewModel(
            INavigationService navigationService,
            IFirestoreRepository<TaskModel> tasksRepository,
            IDateService dateService,
            IDialogService dialogService): base(navigationService)
        {
            _tasksRepository = tasksRepository;
            _dateService = dateService;
            _dialogService = dialogService;

            CheckTaskCommand = new Command<TaskModel>(CheckTaskCommandHandler);
            PreviousWeekCommand = new Command<DateTime>(PreviousWeekCommandHandler);
            NextWeekCommand = new Command<DateTime>(NextWeekCommandHandler);
            DayCommand = new Command<DayModel>(DayCommandHandler);
            AddCommand = new Command(AddCommandHandler);

            ItemDragged = new Command<TaskModel>(OnItemDragged);
            ItemDraggedOver = new Command<TaskModel>(OnItemDraggedOver);
            ItemDragLeave = new Command<TaskModel>(OnItemDragLeave);
            ItemDropped = new Command<TaskModel>(i => OnItemDropped(i));
        }


        #endregion

        #region Command Handlers

        private void CheckTaskCommandHandler(TaskModel task)
        {
            task.archived = !task.archived;
            TaskList = new ObservableCollection<TaskModel>(TaskList.OrderBy(t => t.archived).ToList());
        }

        private async void DayCommandHandler(DayModel day)
        {
            ResetActiveDay();
            day.State = DayStateEnum.Active;
            await GetTasksByDate(day.Date);
        }

        private void PreviousWeekCommandHandler(DateTime startDate)
        {
            ResetActiveDay();
            Week = _dateService.GetWeek(startDate.AddDays(-1));
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));
        }

        private void NextWeekCommandHandler(DateTime lastDate)
        {
            ResetActiveDay();
            Week = _dateService.GetWeek(lastDate.AddDays(1));
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));
        }

        private void AddCommandHandler()
        {
            _dialogService.ShowDialog(nameof(AddDialog), null, async (param) => {
                var option = param.Parameters.GetValue<string>("option");
                if(option != null)
                {
                    if (option == "Add a task")
                    {
                        await _navigationService.NavigateAsync(nameof(AddTaskPage));
                    }
                    else
                    {
                        await _navigationService.NavigateAsync(nameof(AddProjectPage));
                    }
                }
            });
        }

        private void OnItemDragged(TaskModel item)
        {
            Debug.WriteLine($"OnItemDragged: {item?.task}");
            TaskList.ForEach(i => i.isBeingDragged = item == i);
        }

        private void OnItemDraggedOver(TaskModel item)
        {
            Debug.WriteLine($"OnItemDraggedOver: {item?.task}");
            var itemBeingDragged = TaskList.FirstOrDefault(i => i.isBeingDragged);
            TaskList.ForEach(i => i.isBeingDraggedOver = item == i && item != itemBeingDragged);
        }

        private void OnItemDragLeave(TaskModel item)
        {
            Debug.WriteLine($"OnItemDragLeave: {item?.task}");
            TaskList.ForEach(i => i.isBeingDraggedOver = false);
        }

        private async Task OnItemDropped(TaskModel item)
        {
            var itemToMove = TaskList.First(i => i.isBeingDragged);
            var itemToInsertBefore = item;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            var insertAtIndex = TaskList.IndexOf(itemToInsertBefore);
            TaskList.Remove(itemToMove);
            TaskList.Insert(insertAtIndex, itemToMove);
            itemToMove.isBeingDragged = false;
            itemToInsertBefore.isBeingDraggedOver = false;
        }


        #endregion

        #region Navigation

        #endregion

        #region Private Methods

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Week = _dateService.GetWeek(DateTime.Now);
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));

            SetUserName();
            await GetTasksByDate(DateTime.Now);
        }

        private void SetUserName()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            Name = auth.GetUsername();
        }

        private void ResetActiveDay()
        {
            var selectedDay = DaysList.FirstOrDefault(d => d.State.Equals(DayStateEnum.Active));
            if (selectedDay != null)
            {
                selectedDay.State = selectedDay.Date < DateTime.Now.Date ? DayStateEnum.Past : DayStateEnum.Normal;
            }
        }

        private async Task GetTasksByDate(DateTime date)
        {
            TaskListState = LayoutState.Loading;
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var userId = auth.GetUserId();
            var taskList = await _tasksRepository.GetAllContains(userId, "date", date.ToString("dd/MM/yyyy"));
            TaskList = new ObservableCollection<TaskModel>(taskList.OrderBy(t => t.archived).ToList());
            if(TaskList.Count == 0)
            {
                TaskListState = LayoutState.Empty;
            }
            else
            {
                TaskListState = LayoutState.None;
            }
        }

        #endregion
    }
}
