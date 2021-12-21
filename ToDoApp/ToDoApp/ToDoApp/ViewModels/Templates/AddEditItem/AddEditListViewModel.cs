using Prism.Navigation;
using Prism.Regions.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Helpers.Validations;
using ToDoApp.Helpers.Validations.Rules;
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
        public ValidatableObject<string> Name { get; set; }
        public ValidatableObject<string> Color { get; set; }
        public ObservableCollection<string> ColorList { get; set; }
        public string Mode { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; set; }

        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public AddEditListViewModel(
            INavigationService navigationService,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _listRepository = listRepository;

            CreateCommand = new Command(CreateCommandHandler);
            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();

            InitForm();
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "name": Name.Validate(); break;
                case "color": Color.Validate(); break;
            }
        }

        #endregion

        #region Command Handlers

        private async void CreateCommandHandler()
        {
            ValidateForm();
            if (!IsFormValid())
            {
                return;
            }

            try
            {
                var auth = DependencyService.Get<IFirebaseAuthentication>();
                var userId = auth.GetUserId();
                var model = new ListModel()
                {
                    Name = Name.Value,
                    Color = Color.Value,
                    UserId = userId
                };
                var wasAdded = await _listRepository.Add(model);
                if(wasAdded)
                {
                    await _navigationService.GoBackAsync();
                }
                else
                {
                    //display error message
                }
            }
            catch (Exception ex)
            {
                //display error message
                Debug.Write(ex.Message);
            }
        }

        #endregion

        #region Region Navigation Handlers

        public override void OnNavigatedTo(INavigationContext navigationContext)
        {
            var isEdit = navigationContext.Parameters.GetValue<bool>("isEdit");
            var list = navigationContext.Parameters.GetValue<ListModel>("list");

            Mode = isEdit ? "Edit" : "Add";

            if (Mode == "Edit")
            {
                AddList = new ListModel()
                {
                    Name = Constants.DefaultList.Name,
                    Color = Constants.DefaultList.Color,
                };
            }
            else
            {
                AddList = new ListModel()
                {
                    Name = Constants.DefaultList.Name,
                    Color = Constants.DefaultList.Color,
                };
            }
        }

        #endregion

        #region Private Methods

        private void InitForm()
        {
            ColorList = new ObservableCollection<string>(Constants.ListColorList);
            Color.Value = Constants.DefaultList.Color;
        }

        private bool IsFormValid()
        {
            return Name.IsButtonActive;
        }

        private void ValidateForm()
        {
            Name.Validate();
            Color.Validate();
        }

        private void AddValidations()
        {
            Name = new ValidatableObject<string>();
            Color = new ValidatableObject<string>();

            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });
            Color.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A color is required." });
        }

        #endregion
    }
}
