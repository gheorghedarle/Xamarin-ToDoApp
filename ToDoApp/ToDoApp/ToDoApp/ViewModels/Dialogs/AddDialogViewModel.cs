using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using ToDoApp.Helpers;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Dialogs
{
    public class AddDialogViewModel : BaseViewModel, IDialogAware
    {
        public ObservableCollection<string> OptionsList { get; set; }

        public Command CloseCommand { get; set; }

        #region Constructors

        public AddDialogViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            CloseCommand = new Command(CloseCommandHandler);

            OptionsList = MenuHelper.AddOptions;
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
