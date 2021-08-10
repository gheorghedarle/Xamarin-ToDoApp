using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates
{
    public class AddTaskViewModel : BindableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel AddTask { get; set; }

        public AddTaskViewModel()
        {
            AddTask = Constants.DefaultTask;
        }
    }
}
