using Prism.Navigation;
using Prism.Services.Dialogs;
using System;

namespace ToDoApp.ViewModels.Dialogs
{
    public class AddTaskDialogViewModel : BaseViewModel, IDialogAware
    {
        public AddTaskDialogViewModel(
            INavigationService navigationService) : base(navigationService)
        { }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
