using Prism.Navigation;
using Prism.Regions.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates.AddEditItem
{
    public class AddEditListViewModel : BaseRegionViewModel
    {
        #region Private & Protected

        private IFirestoreRepository<ListModel> _listRepository;

        #endregion

        #region Properties

        public ListModel AddList { get; set; }
        public ObservableCollection<ColorModel> ColorList { get; set; }
        public string Mode { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; set; }

        #endregion

        #region Constructors

        public AddEditListViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _listRepository = listRepository;

            CreateCommand = new Command(CreateCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void CreateCommandHandler()
        {
            try
            {
                var auth = DependencyService.Get<IFirebaseAuthentication>();
                var userId = auth.GetUserId();
                var model = new ListModel()
                {
                    name = AddList.name,
                    color = AddList.colorObject.color,
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

        #endregion

        #region Region Navigation Handlers

        public new void OnNavigatedTo(INavigationContext navigationContext)
        {
            var isEdit = navigationContext.Parameters.GetValue<bool>("isEdit");
            var list = navigationContext.Parameters.GetValue<ListModel>("list");

            Mode = isEdit ? "Edit" : "Add";
            ColorList = new ObservableCollection<ColorModel>(Constants.ListColorList);

            if (Mode == "Edit")
            {
                AddList = new ListModel()
                {
                    name = Constants.DefaultList.name,
                    colorObject = Constants.DefaultList.colorObject,
                };
            }
            else
            {
                AddList = new ListModel()
                {
                    name = Constants.DefaultList.name,
                    colorObject = Constants.DefaultList.colorObject,
                };
            }
        }

        #endregion
    }
}
