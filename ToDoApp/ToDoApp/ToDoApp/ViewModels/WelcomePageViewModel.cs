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
                "Task Number 1", "Task Number 2"
            };
        }

    }
}
