using Prism.Navigation;
using Prism.Services.Dialogs;
using System;

namespace ToDoApp.ViewModels.Dialogs
{
    public class AddTaskDialogViewModel : BaseViewModel, IDialogAware
    {
        #region Constructors

        public AddTaskDialogViewModel(
            INavigationService navigationService) : base(navigationService)
        { }

        #endregion

        #region Dialog

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog()
        { return true; }

        public void OnDialogClosed()
        { }

        public void OnDialogOpened(IDialogParameters parameters)
        { }

        #endregion
    }
}
