using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoApp.Models;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class TasksPageViewModel: BaseViewModel
    {
        public ObservableCollection<TasksModel> TaskList { get; set; }

        public TasksPageViewModel()
        {
            TaskList = new ObservableCollection<TasksModel>()
            {
                new TasksModel() { Title = "Title 1", Description = "Description", IsDone = true },
                new TasksModel() { Title = "Title 2", Description = "Description", IsDone = false },
                new TasksModel() { Title = "Title 3", Description = "Description", IsDone = true },
                new TasksModel() { Title = "Title 4", Description = "Description", IsDone = false },
                new TasksModel() { Title = "Title 5", Description = "Description", IsDone = true },
            };
        }
    }
}
