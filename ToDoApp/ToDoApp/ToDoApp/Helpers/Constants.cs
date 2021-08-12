using System;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoApp.Models;

namespace ToDoApp.Helpers
{
    public static class Constants
    {
        public static ObservableCollection<string> AddOptions = new ObservableCollection<string>() {
            "task",
            "list"
        };

        public static ObservableCollection<ColorModel> ListColorList = new ObservableCollection<ColorModel>() {
            new ColorModel() { Name="Red", Color = "#F9371C" },
            new ColorModel() { Name="Orange", Color = "#F97C1C" },
            new ColorModel() { Name="Yellow", Color = "#F9C81C" },
            new ColorModel() { Name="Turquoise", Color = "#41D0B6" },
            new ColorModel() { Name="Cyan", Color = "#2CADF6" },
            new ColorModel() { Name="Purple", Color = "#6562FC" },
        };

        public static ListModel InboxList = new ListModel() {
            id = "zlDZNn3sNmyirSNs3mRY",
            name = "Inbox",
            userId = "Default",
            color = "#F9371C"
        };

        public static TaskModel DefaultTask = new TaskModel()
        {
            task = "Test",
            archived = false,
            date = DateTime.Today.ToString("dd/MM/yyyy"),
            dateObject = DateTime.Today,
            listObject = InboxList,
            list = "Inbox"
        };

        public static ListModel DefaultList = new ListModel()
        {
            name = "Test 2",
            color = "#F9371C",
            colorObject = ListColorList.First()
        };
    }
}
