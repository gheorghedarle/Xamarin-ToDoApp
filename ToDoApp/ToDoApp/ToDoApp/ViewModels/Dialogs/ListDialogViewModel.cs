using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private string _list;
        private string _fromPage;

        private IFirestoreRepository<ListModel> _listRepository;

        #endregion

        #region Properties

        public bool HasError { get; set; }
        public ObservableCollection<ListModel> ProjectList { get; set; }
        public ListModel SelectedList { get; set; }

        #endregion

        #region Commands 

        public ICommand ChangeSelectListCommand { get; set; }

        #endregion

        #region Constructors

        public ListDialogViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _listRepository = listRepository;

            ChangeSelectListCommand = new Command(ChangeSelectListCommandHandler);

            MainState = LayoutState.Loading;
        }

        #endregion

        #region Command Handlers

        private void ChangeSelectListCommandHandler()
        {
            var list = ProjectList.First(a => a.name == _list);
            if(SelectedList != list)
            {
                if(_fromPage == "More")
                {
                    Preferences.Set("taskFilterByList", SelectedList.name);
                }
                var param = new DialogParameters()
                {
                    { "selectedList", SelectedList.name }
                };
                RequestClose(param);
            }
        }

        #endregion

        #region Dialog

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        { }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            _fromPage = parameters.GetValue<string>("fromPage");
            var selectedItem = parameters.GetValue<string>("selectedItem");
            var projectList = await GetProjectList(_fromPage);
            ProjectList = new ObservableCollection<ListModel>(projectList);

            if(selectedItem == null)
            {
                _list = Preferences.Get("taskFilterByList", "All lists");
                SelectedList = ProjectList.First(a => a.name == _list);
            }
            else
            {
                _list = selectedItem;
                SelectedList = ProjectList.First(a => a.name == selectedItem);
            }

            MainState = LayoutState.None;
        }

        #endregion

        #region Private Methods

        private async Task<List<ListModel>> GetProjectList(string fromPage)
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
                if(fromPage == "More")
                {
                    listToAdd.Insert(0, Constants.AllLists);
                }
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
