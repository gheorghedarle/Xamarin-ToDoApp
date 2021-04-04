using Plugin.CloudFirestore;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel: BaseViewModel
    {
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
            INavigationService navigationService): base(navigationService)
        {
            CheckTaskCommand = new Command<TaskModel>(CheckTaskCommandHandler);
            PreviousWeekCommand = new Command<DateTime>(PreviousWeekCommandHandler);
            NextWeekCommand = new Command<DateTime>(NextWeekCommandHandler);
            DayCommand = new Command<DayModel>(DayCommandHandler);
            AddTaskCommand = new Command(AddTaskCommandHandler);

            Week = DateService.GetWeek(DateTime.Now);
            DaysList = new ObservableCollection<DayModel>(DateService.GetDayList(Week.StartDay, Week.LastDay));

            SetUserName();
        }

        private void CheckTaskCommandHandler(TaskModel task)
        {
            task.archived = !task.archived;
            TaskList = new ObservableCollection<TaskModel>(TaskList.OrderBy(t => t.archived).ToList());
        }

        private void DayCommandHandler(DayModel day)
        {
            ResetActiveDay();
            day.IsActive = true;
        }

        private void PreviousWeekCommandHandler(DateTime startDate)
        {
            ResetActiveDay();
            Week = DateService.GetWeek(startDate.AddDays(-1));
            DaysList = new ObservableCollection<DayModel>(DateService.GetDayList(Week.StartDay, Week.LastDay));
        }

        private void NextWeekCommandHandler(DateTime lastDate)
        {
            ResetActiveDay();
            Week = DateService.GetWeek(lastDate.AddDays(1));
            DaysList = new ObservableCollection<DayModel>(DateService.GetDayList(Week.StartDay, Week.LastDay));
        }

        private void AddTaskCommandHandler()
        {
            Debug.WriteLine("Add Task Button");
        }

        private void SetUserName()
        {
            Name = Preferences.Get("Name", "");
        }

        private void ResetActiveDay()
        {
            var selectedDay = DaysList.FirstOrDefault(d => d.IsActive);
            if (selectedDay != null)
            {
                selectedDay.IsActive = false;
            }
        }
    }
}
