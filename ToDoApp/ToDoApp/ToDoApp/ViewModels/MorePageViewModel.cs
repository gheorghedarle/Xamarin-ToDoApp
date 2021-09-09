using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class MorePageViewModel : 
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IFirestoreRepository<ListModel> _listRepository;

        #endregion

        #region Properties

        public ObservableCollection<ListModel> ProjectList { get; set; }
        public ListModel SelectedList { get; set; }

        #endregion

        #region Commands

        public ICommand BackCommand { get; set; }

        #endregion

        #region Constructors

        public MorePageViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _listRepository = listRepository;

            BackCommand = new Command(BackCommandHandler);
        }

        public async void Initialize(INavigationParameters parameters)
        {
            var projectList = await GetProjectList();
            ProjectList = new ObservableCollection<ListModel>(projectList);

            SelectedList = ProjectList.First();
        }

        #endregion

        #region Private Methods

        private async Task<List<ListModel>> GetProjectList()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var userId = auth.GetUserId();

            var querySnapshot = await _listRepository.GetAll(userId).GetAsync();
            var list = querySnapshot.ToObjects<ListModel>();
            var listToAdd = new List<ListModel>();
            if (list.Count() > 0)
            {
                listToAdd = list.ToList();
                listToAdd.Insert(0, Constants.InboxList);
                listToAdd.Insert(0, Constants.AllLists);
            }
            else
            {
                listToAdd.Add(Constants.InboxList);
            }
            return listToAdd;
        }

        #endregion
    }
}
