using Prism.Mvvm;
using Prism.Navigation;
using System.ComponentModel;
using ToDoApp.Helpers;
using ToDoApp.Models;

namespace ToDoApp.ViewModels.Templates
{
    public class AddTaskViewModel : BindableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel AddTask { get; set; }

        public AddTaskViewModel() : base()
        {
            AddTask = Constants.DefaultTask;
        }
    }
}
