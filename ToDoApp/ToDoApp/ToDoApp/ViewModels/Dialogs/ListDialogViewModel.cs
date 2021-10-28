using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Dialogs
{
    public class ListDialogViewModel : BaseViewModel, IDialogAware
    {
        #region Private & Protected

        private IFirestoreRepository<ListModel> _listRepository;

        #endregion

        #region Properties

        public bool HasError { get; set; }
        public ObservableCollection<ListModel> ProjectList { get; set; }
        public ListModel SelectedList { get; set; }

        #endregion

        #region Constructors

        public ListDialogViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _listRepository = listRepository;

            MainState = LayoutState.Loading;
        }

        #endregion

        #region Command Handlers

        #endregion

        #region Dialog

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        { }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            var projectList = await GetProjectList();
            ProjectList = new ObservableCollection<ListModel>(projectList);

            var list = Preferences.Get("taskFilterByList", "All lists");
            SelectedList = list == "all" ? Constants.AllLists : ProjectList.First(a => a.name == list);

            MainState = LayoutState.None;
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
