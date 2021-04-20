using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Dialogs
{
    public class AddTaskDialogViewModel : BaseViewModel, IDialogAware
    {
        public Command CloseCommand { get; set; }

        #region Constructors

        public AddTaskDialogViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            CloseCommand = new Command(CloseCommandHandler);
        }

        #endregion

        private void CloseCommandHandler()
        {
            RequestClose(null);
        }

        #region Dialog

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        { }

        public void OnDialogOpened(IDialogParameters parameters)
        { }

        #endregion
    }
}
