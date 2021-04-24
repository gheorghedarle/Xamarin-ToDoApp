using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using ToDoApp.Helpers;
using ToDoApp.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels.Dialogs
{
    public class AddDialogViewModel : BaseViewModel, IDialogAware
    {
        public ObservableCollection<string> OptionsList { get; set; }

        public Command CloseCommand { get; set; }
        public Command AddCommand { get; set; }

        #region Constructors

        public AddDialogViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            CloseCommand = new Command(CloseCommandHandler);
            AddCommand = new Command<string>(AddCommandHandler);

            OptionsList = MenuHelper.AddOptions;
        }

        #endregion

        private void CloseCommandHandler()
        {
            RequestClose(null);
        }

        private async void AddCommandHandler(string option)
        {
            if(option == "Add a task")
            {
                await _navigationService.NavigateAsync(nameof(AddTaskPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(AddProjectPage));
            }
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
