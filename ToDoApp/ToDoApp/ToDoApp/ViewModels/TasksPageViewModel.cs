using Prism.Navigation;
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
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Plugin.CloudFirestore.Reactive;
using System.Reactive.Linq;
using Plugin.CloudFirestore;
using System.Reactive.Disposables;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel : 
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IDateService _dateService;
        private IFirestoreRepository<TaskModel> _tasksRepository;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private DayModel _selectedDay;

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
            IDateService dateService): base(navigationService)
        {
            _tasksRepository = tasksRepository;
            _dateService = dateService;

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

        public void Initialize(INavigationParameters parameters)
        {
            TaskListState = LayoutState.Loading;

            Week = _dateService.GetWeek(DateTime.Now);
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));
            TaskList = new ObservableCollection<TaskModel>();
            _selectedDay = new DayModel() { Date = DateTime.Today };

            SetUserName();
            CreateQueryForTasks(DateTime.Today);
            SetActiveDay();
        }

        #endregion

        #region Command Handlers

        private void CheckTaskCommandHandler(TaskModel task)
        {
            task.archived = !task.archived;
            _tasksRepository.Update(task);
        }

        private void DayCommandHandler(DayModel day)
        {
            SetActiveDay(day);
            CreateQueryForTasks(day.Date);
        }

        private void PreviousWeekCommandHandler(DateTime startDate)
        {
            Week = _dateService.GetWeek(startDate.AddDays(-1));
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));
            SetActiveDay();
        }

        private void NextWeekCommandHandler(DateTime lastDate)
        {
            Week = _dateService.GetWeek(lastDate.AddDays(1));
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));
            SetActiveDay();
        }

        private void AddCommandHandler()
        {
            _navigationService.NavigateAsync(nameof(AddPage));
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

        private Task OnItemDropped(TaskModel item)
        {
            var itemToMove = TaskList.First(i => i.isBeingDragged);
            var itemToInsertBefore = item;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return Task.CompletedTask;

            var insertAtIndex = TaskList.IndexOf(itemToInsertBefore);
            TaskList.Remove(itemToMove);
            TaskList.Insert(insertAtIndex, itemToMove);
            itemToMove.isBeingDragged = false;
            itemToInsertBefore.isBeingDraggedOver = false;
            return Task.CompletedTask;
        }


        #endregion

        #region Private Methods

        private void CreateQueryForTasks(DateTime date)
        {
            TaskListState = LayoutState.Loading;
            _disposables.Clear();
            TaskList.Clear();
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var userId = auth.GetUserId();
            var query = _tasksRepository.GetAllContains(userId, "date", date.ToString("dd/MM/yyyy"));
            _disposables.Add(query.ObserveAdded()
                .Select(change => (Object: change.Document.ToObject<TaskModel>(ServerTimestampBehavior.Estimate), Index: change.NewIndex))
                .Subscribe(t =>
                {
                    TaskList.Insert(t.Index, t.Object);
                }));
            _disposables.Add(query.ObserveModified()
                 .Select(change => change.Document.ToObject<TaskModel>(ServerTimestampBehavior.Estimate))
                 .Select(taskItem => (TaskItem: taskItem, ViewModel: TaskList.FirstOrDefault(x => x.id == taskItem.id)))
                 .Where(t => t.ViewModel != null)
                 .Subscribe(t =>
                 {
                     t.ViewModel.Update(t.TaskItem);
                 }));
            _disposables.Add(query.ObserveRemoved()
                 .Select(change => TaskList.FirstOrDefault(x => x.id == change.Document.Id))
                 .Subscribe(viewModel =>
                 {
                     TaskList.Remove(viewModel);
                 }));
            _disposables.Add(query.AsObservable()
                .Subscribe(list =>
                {
                    if (list.Count == 0)
                    {
                        TaskListState = LayoutState.Empty;
                    }
                    else
                    {
                        TaskListState = LayoutState.None;
                    }
                }));
        }

        private void SetUserName()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            Name = auth.GetUsername();
        }

        private void SetActiveDay(DayModel day = null)
        {
            ResetActiveDay();
            if (day != null)
            {
                _selectedDay = day;
                day.State = DayStateEnum.Active;
            }
            else
            {
                var selectedDate = DaysList.FirstOrDefault(d => d.Date == _selectedDay.Date);
                if(selectedDate != null)
                {
                    selectedDate.State = DayStateEnum.Active;
                }
            }
        }

        private void ResetActiveDay()
        {
            var selectedDay = DaysList?.FirstOrDefault(d => d.State.Equals(DayStateEnum.Active));
            if (selectedDay != null)
            {
                selectedDay.State = selectedDay.Date < DateTime.Now.Date ? DayStateEnum.Past : DayStateEnum.Normal;
            }
        }

        #endregion

        #region Override

        public override void Destroy()
        {
            base.Destroy();
            _disposables.Dispose();
        }

        #endregion
    }
}
