using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates
{
    public class AddListViewModel : BaseViewModel
    {
        private IFirestoreRepository<ListModel> _listRepository;

        private Task Initialization { get; set; }

        public ListModel AddList { get; set; }
        public ObservableCollection<ColorModel> ColorList { get; set; }

        public ICommand CreateCommand { get; set; }

        public AddListViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _listRepository = listRepository;

            CreateCommand = new Command(CreateCommandHandler);

            Initialization = Initialize();
        }
        public Task Initialize()
        {
            ColorList = new ObservableCollection<ColorModel>(Constants.ListColorList);
            AddList = Constants.DefaultList;

            return Task.CompletedTask;
        }

        private async void CreateCommandHandler()
        {
            try
            {
                var auth = DependencyService.Get<IFirebaseAuthentication>();
                var userId = auth.GetUserId();
                var model = new ListModel()
                {
                    name = AddList.name,
                    color = AddList.color,
                    userId = userId
                };
                await _listRepository.Add(model);
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                //display error message
                Debug.Write(ex.Message);
            }
        }
    }
}
