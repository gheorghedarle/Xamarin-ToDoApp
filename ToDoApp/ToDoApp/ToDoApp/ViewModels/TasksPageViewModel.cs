using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public TasksPageViewModel()
        {
            CheckTaskCommand = new Command<TaskModel>(CheckTaskCommandHandler);
            PreviousWeekCommand = new Command<DateTime>(PreviousWeekCommandHandler);
            NextWeekCommand = new Command<DateTime>(NextWeekCommandHandler);
            DayCommand = new Command<DayModel>(DayCommandHandler);

            var taskList = new List<TaskModel>()
            {
                new TaskModel() { Title = "Title 1", Description = "Description", IsDone = true },
                new TaskModel() { Title = "Title 2", Description = "Description", IsDone = false },
                new TaskModel() { Title = "Title 3", Description = "Description", IsDone = true },
                new TaskModel() { Title = "Title 4", Description = "Description", IsDone = false },
                new TaskModel() { Title = "Title 5", Description = "Description", IsDone = true },
            };
            TaskList = new ObservableCollection<TaskModel>(taskList.OrderBy(t => t.IsDone).ToList());

            Week = DateService.GetWeek(DateTime.Now);
            DaysList = new ObservableCollection<DayModel>(DateService.GetDayList(Week.StartDay, Week.LastDay));

            SetUserName();
        }

        private void CheckTaskCommandHandler(TaskModel task)
        {
            task.IsDone = !task.IsDone;
            TaskList = new ObservableCollection<TaskModel>(TaskList.OrderBy(t => t.IsDone).ToList());
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
