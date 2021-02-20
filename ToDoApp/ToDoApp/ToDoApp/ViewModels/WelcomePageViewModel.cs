using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ToDoApp.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        public ObservableCollection<string> TaskList { get; set; }

        public WelcomePageViewModel()
        {
            TaskList = new ObservableCollection<string>()
            {
                "Task Number 1",
                "Task Number 2",
                "Task Number 3",
                "Task Number 4",
                "Task Number 5",
                "Task Number 6",
                "Task Number 7",
                "Task Number 8",
                "Task Number 9",
                "Task Number 10",
                "Task Number 11",
                "Task Number 12",
                "Task Number 13",
                "Task Number 14",
                "Task Number 15"
            };
        }

    }
}
