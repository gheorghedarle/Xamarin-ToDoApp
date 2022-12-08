﻿using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Reactive;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Services.DateService;
using ToDoApp.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel :
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IDateService _dateService;
        private IFirestoreRepository<TaskModel> _taskRepository;
        private IFirestoreRepository<ListModel> _listsRepository;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private DayModel _selectedDay;

        #endregion

        #region Properties

        public ObservableCollection<DayModel> DaysList { get; set; }
        public ObservableCollection<TaskModel> TaskList { get; set; }
        public LayoutState TaskListState { get; set; }
        public string Name { get; set; }
        public WeekModel Week { get; set; }
        public string Filter { get; set; }

        #endregion

        #region Commands

        public ICommand CheckTaskCommand { get; set; }
        public ICommand DayCommand { get; set; }
        public ICommand PreviousWeekCommand { get; set; }
        public ICommand NextWeekCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }

        public ICommand ItemDragged { get; }
        public ICommand ItemDraggedOver { get; }
        public ICommand ItemDragLeave { get; }
        public ICommand ItemDropped { get; }

        #endregion

        #region Constructors

        public TasksPageViewModel(
            INavigationService navigationService,
            IFirestoreRepository<TaskModel> tasksRepository,
            IFirestoreRepository<ListModel> listsRepository,
            IDateService dateService) : base(navigationService)
        {
            _taskRepository = tasksRepository;
            _listsRepository = listsRepository;
            _dateService = dateService;

            CheckTaskCommand = new Command<TaskModel>(CheckTaskCommandHandler);
            PreviousWeekCommand = new Command<DateTime>(PreviousWeekCommandHandler);
            NextWeekCommand = new Command<DateTime>(NextWeekCommandHandler);
            DayCommand = new Command<DayModel>(DayCommandHandler);
            AddCommand = new Command(AddCommandHandler);
            DeleteTaskCommand = new Command<TaskModel>(DeleteTaskCommandHandler);
            EditTaskCommand = new Command<TaskModel>(EditTaskCommandHandler);
            ProfileCommand = new Command(ProfileCommandHandler);
            MoreCommand = new Command(MoreCommandHandler);

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
            task.Archived = !task.Archived;
            _taskRepository.Update(task);
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
            _navigationService.NavigateAsync(nameof(AddEditPage));
        }

        private void MoreCommandHandler()
        {
            _navigationService.NavigateAsync(nameof(MorePage));
        }

        private void DeleteTaskCommandHandler(TaskModel taskModel)
        {
           _taskRepository.Delete(taskModel);
        }

        private async void EditTaskCommandHandler(TaskModel taskModel)
        {
            var param = new NavigationParameters()
            {
                { "task", taskModel }
            };
            await _navigationService.NavigateAsync(nameof(AddEditPage), param);
        }

        private void ProfileCommandHandler()
        {
            _navigationService.NavigateAsync(nameof(ProfilePage));
        }

        private void OnItemDragged(TaskModel item)
        {
            Debug.WriteLine($"OnItemDragged: {item?.Task}");
            TaskList.ForEach(i => i.isBeingDragged = item == i);
        }

        private void OnItemDraggedOver(TaskModel item)
        {
            Debug.WriteLine($"OnItemDraggedOver: {item?.Task}");
            var itemBeingDragged = TaskList.FirstOrDefault(i => i.isBeingDragged);
            TaskList.ForEach(i => i.isBeingDraggedOver = item == i && item != itemBeingDragged);
        }

        private void OnItemDragLeave(TaskModel item)
        {
            Debug.WriteLine($"OnItemDragLeave: {item?.Task}");
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

        #region Navigation

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == Prism.Navigation.NavigationMode.Back)
            {
                CreateQueryForTasks(_selectedDay.Date);
            }
        }

        #endregion

        #region Private Methods

        private void CreateQueryForTasks(DateTime date)
        {
            try
            {
                TaskListState = LayoutState.Loading;
                _disposables.Clear();
                TaskList.Clear();
                IQuery query;
                var auth = DependencyService.Get<IFirebaseAuthentication>();
                var userId = auth.GetUserId();
                var list = Preferences.Get("taskFilterByList", "All lists");
                var hideDoneTask = Preferences.Get("hideDoneTasks", false);
                SetFilterName(list);
                query = list == "All lists" ?
                    hideDoneTask == false ?
                    _taskRepository.GetAllContains(userId, "date", date.ToString("dd/MM/yyyy")) :
                    _taskRepository.GetAllContains(userId, "date", date.ToString("dd/MM/yyyy"), "archived", !hideDoneTask) :
                    hideDoneTask == false ?
                    _taskRepository.GetAllContains(userId, "date", date.ToString("dd/MM/yyyy"), "list", list) :
                    _taskRepository.GetAllContains(userId, "date", date.ToString("dd/MM/yyyy"), "list", list, "archived", !hideDoneTask);
                _disposables.Add(query.ObserveAdded()
                    .Select(change => (TaskItem: change.Document.ToObject<TaskModel>(ServerTimestampBehavior.Estimate), Index: change.NewIndex, DocumentChange: change))
                    .Subscribe(async t =>
                    {
                        var listRes = t.DocumentChange.Document.Data.TryGetValue("list", out var list);
                        if(listRes)
                        {
                            var listObject = await _listsRepository.Get(list.ToString());
                            var task = t.TaskItem;
                            task.ListObj = listObject;
                            TaskList.Add(task);
                        }
                    }));
                _disposables.Add(query.ObserveModified()
                     .Select(change => (Object: change.Document.ToObject<TaskModel>(ServerTimestampBehavior.Estimate), DocumentChange: change))
                     .Select(taskItem => (TaskItem: taskItem.Object, ViewModel: TaskList.FirstOrDefault(x => x.Id == taskItem.Object.Id), DocumentChange: taskItem.DocumentChange))
                     .Where(t => t.ViewModel != null)
                     .Subscribe(async t =>
                     {
                         var listRes = t.DocumentChange.Document.Data.TryGetValue("list", out var list);
                         if (listRes)
                         {
                             var listObject = await _listsRepository.Get(list.ToString());
                             var task = t.TaskItem;
                             task.ListObj = listObject;
                             t.ViewModel.Update(task);
                         }
                     }));
                _disposables.Add(query.ObserveRemoved()
                     .Select(change => TaskList.FirstOrDefault(x => x.Id == change.Document.Id))
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
            catch (Exception ex)
            {
                MainState = LayoutState.Error;
                Debug.WriteLine(ex.Message);
            }
        }

        private void SetUserName()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            Name = auth.GetUsername();
        }

        private void SetFilterName(string list)
        {
            Filter = list == "all" ?
                "All lists" :
                list;
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
                if (selectedDate != null)
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
