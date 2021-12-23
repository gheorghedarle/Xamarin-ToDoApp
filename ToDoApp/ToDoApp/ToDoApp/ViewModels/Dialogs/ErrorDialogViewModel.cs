using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Dialogs
{
    class ErrorDialogViewModel: BaseViewModel, IDialogAware
    {
        #region Properties

        public string Message { get; set; }

        #endregion

        #region Commands 

        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructors

        public ErrorDialogViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            CloseCommand = new Command(CloseCommandHandler);
        }

        #endregion

        #region Command Handlers

        private void CloseCommandHandler()
        {
            RequestClose(null);
        }

        #endregion

        #region Dialog

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var message = parameters.GetValue<string>("message");

            Message = message;
        }

        #endregion
    }
}
