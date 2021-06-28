using System;
using System.Collections.ObjectModel;
using ToDoApp.Models;
using Xamarin.Forms;

namespace ToDoApp.Helpers
{
    public static class Constants
    {
        public static ListModel InboxList = new ListModel() {
            id = "zlDZNn3sNmyirSNs3mRY",
            name = "Inbox",
            userId = "Default",
        };

        public static TaskModel DefaultTask = new TaskModel()
        {
            archived = false,
            date = DateTime.Today.ToString("dd/MM/yyyy"),
            list = "Inbox",
        };

        public static ListModel DefaultList = new ListModel()
        {
            name = "",
        };

        public static ObservableCollection<string> AddOptions = new ObservableCollection<string>() {
            "task",
            "list"
        };

        public static ObservableCollection<Color> ListColorList = new ObservableCollection<Color>() {
            Color.FromHex("#F9371C"),
            Color.FromHex("#F97C1C"),
            Color.FromHex("#F9C81C"),
            Color.FromHex("#41D0B6"),
            Color.FromHex("#2CADF6"),
            Color.FromHex("#6562FC"),
        };
    }
}
