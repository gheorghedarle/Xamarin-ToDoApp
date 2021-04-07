using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Services.DateService;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel: BaseViewModel
    {
        private IDateService _dateService;
        private IFirestoreRepository<TaskModel> _tasksRepository;

        public ObservableCollection<DayModel> DaysList { get; set; }
        public ObservableCollection<TaskModel> TaskList { get; set; }

        public string Name { get; set; }
        public WeekModel Week { get; set; }

        public ICommand CheckTaskCommand { get; set; }
        public ICommand DayCommand { get; set; }
        public ICommand PreviousWeekCommand { get; set; }
        public ICommand NextWeekCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }

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
            AddTaskCommand = new Command(AddTaskCommandHandler);
        }

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

        private void AddTaskCommandHandler()
        {
            Debug.WriteLine("Add Task Button");
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Week = _dateService.GetWeek(DateTime.Now);
            DaysList = new ObservableCollection<DayModel>(_dateService.GetDayList(Week.StartDay, Week.LastDay));

            SetUserName();
            await GetTasksByDate(DateTime.Now);
        }

        private void SetUserName()
        {
            Name = Preferences.Get("Name", "");
        }

        private void ResetActiveDay()
        {
            var selectedDay = DaysList.FirstOrDefault(d => d.State.Equals(DayStateEnum.Active));
            if (selectedDay != null)
            {
                selectedDay.State = DayStateEnum.Future;
            }
        }

        private async Task GetTasksByDate(DateTime date)
        {
            var taskList = await _tasksRepository.GetAllContains("date", date.ToString("dd/MM/yyyy"));
            TaskList = new ObservableCollection<TaskModel>(taskList.OrderBy(t => t.archived).ToList());
        }
    }
}
