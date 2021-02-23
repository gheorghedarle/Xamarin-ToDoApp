using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoApp.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        public ICommand StartCommand { get; set; }

        public WelcomePageViewModel()
        {
            StartCommand = new Command(StartCommandHandler);
        }

        private async void StartCommandHandler()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TasksPage());
        }
    }
}
