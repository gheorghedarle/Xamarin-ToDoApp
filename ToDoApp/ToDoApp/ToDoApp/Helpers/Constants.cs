using System;
using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.Helpers
{
    public static class Constants
    {
        public static ListModel InboxList = new ListModel() {
            id = "zlDZNn3sNmyirSNs3mRY",
            name = "Inbox",
            userId = "Default",
            color = "#F9371C"
        };

        public static TaskModel DefaultTask = new TaskModel()
        {
            task = "Hello ViewModel",
            archived = false,
            date = DateTime.Today.ToString("dd/MM/yyyy"),
            list = "Inbox"
        };

        public static ListModel DefaultList = new ListModel()
        {
            name = "",
            color = "#F9371C"
        };

        public static ObservableCollection<string> AddOptions = new ObservableCollection<string>() {
            "task",
            "list"
        };

        public static ObservableCollection<string> ListColorList = new ObservableCollection<string>() {
            "#F9371C",
            "#F97C1C",
            "#F9C81C",
            "#41D0B6",
            "#2CADF6",
            "#6562FC",
        };
    }
}
