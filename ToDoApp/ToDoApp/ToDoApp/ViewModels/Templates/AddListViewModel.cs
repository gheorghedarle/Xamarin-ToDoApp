using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ToDoApp.Helpers;
using ToDoApp.Models;

namespace ToDoApp.ViewModels.Templates
{
    public class AddListViewModel : BaseViewModel
    {
        public Task Initialization { get; private set; }

        public ListModel AddList { get; set; }
        public ObservableCollection<ColorModel> ColorList { get; set; }

        public AddListViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Initialization = Initialize();
        }
        public Task Initialize()
        {
            ColorList = new ObservableCollection<ColorModel>(Constants.ListColorList);
            AddList = Constants.DefaultList;

            return Task.CompletedTask;
        }
    }
}
