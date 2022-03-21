using Prism.Events;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Diagnostics;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Events;
using ToDoApp.Helpers;
using ToDoApp.Helpers.Validations;
using ToDoApp.Helpers.Validations.Rules;
using ToDoApp.Views.Dialogs;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Templates.Auth
{
    public class ResetPasswordViewModel : BaseRegionViewModel
    {
        #region Private & Protected

        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ValidatableObject<string> Email { get; set; }

        #endregion

        #region Commands

        public ICommand ResetCommand { get; set; }
        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public ResetPasswordViewModel(
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base(navigationService)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            ResetCommand = new Command(ResetCommandHandler);

            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "email": Email.Validate(); break;
            }
        }

        #endregion

        #region Command Handlers

        private async void ResetCommandHandler()
        {
            try
            {
                MainState = LayoutState.Loading;
                if (ValidateResetData())
                {
                    var auth = DependencyService.Get<IFirebaseAuthentication>();
                    await auth.ForgetPassword(Email.Value).ContinueWith((res) =>
                    {
                        _eventAggregator.GetEvent<SwitchViewEvent>().Publish("Login");
                        ClearAuthData();
                    });
                }
            }
            catch (Exception ex)
            {
                var param = new DialogParameters()
                {
                    { "message", Constants.Errors.GeneralError }
                };
                _dialogService.ShowDialog(nameof(ErrorDialog), param);
                Debug.WriteLine(ex);
            }
            finally
            {
                MainState = LayoutState.None;
            }
        }

        #endregion

        #region Private Functionality

        private void AddValidations()
        {
            Email = new ValidatableObject<string>();

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A email is required." });
            Email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email format is not correct" });
        }

        private bool ValidateResetData()
        {
            if (Email.IsValid == false)
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Email.Value = string.Empty;
        }

        #endregion
    }
}
