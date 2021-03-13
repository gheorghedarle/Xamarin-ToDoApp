using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel: BaseViewModel
    {
        public ObservableCollection<MonthModel> MonthList { get; set; }
        public ObservableCollection<DayModel> DaysList { get; set; }
        public ObservableCollection<TaskModel> TaskList { get; set; }

        public string Name { get; set; }

        public ICommand CheckTaskCommand { get; set; }

        public TasksPageViewModel()
        {
            CheckTaskCommand = new Command<TaskModel>(CheckTaskCommandHandler);

            TaskList = new ObservableCollection<TaskModel>()
            {
                new TaskModel() { Title = "Title 1", Description = "Description", IsDone = true },
                new TaskModel() { Title = "Title 2", Description = "Description", IsDone = false },
                new TaskModel() { Title = "Title 3", Description = "Description", IsDone = true },
                new TaskModel() { Title = "Title 4", Description = "Description", IsDone = false },
                new TaskModel() { Title = "Title 5", Description = "Description", IsDone = true },
            };

            MonthList = new ObservableCollection<MonthModel>(DateService.GetMonthList());
            DaysList = new ObservableCollection<DayModel>(DateService.GetDayList());

            SetUserName();
        }

        private void CheckTaskCommandHandler(TaskModel task)
        {
            task.IsDone = !task.IsDone;
        }

        private void SetUserName()
        {
            Name = Preferences.Get("Name", "");
        }
    }
}
