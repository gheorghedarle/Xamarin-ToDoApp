using System;
using System.Collections.Generic;
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

        public static List<ColorModel> ListColorList = new List<ColorModel>() {
            new ColorModel() { name="Red", color = "#F9371C" },
            new ColorModel() { name="Orange", color = "#F97C1C" },
            new ColorModel() { name="Yellow", color = "#F9C81C" },
            new ColorModel() { name="Turquoise", color = "#41D0B6" },
            new ColorModel() { name="Cyan", color = "#2CADF6" },
            new ColorModel() { name="Purple", color = "#6562FC" },
        };

        public static ListModel InboxList = new ListModel() {
            id = "zlDZNn3sNmyirSNs3mRY",
            name = "Inbox",
            userId = "Default",
            color = "#F9371C"
        };

        public static ListModel AllLists = new ListModel()
        {
            id = "alllist",
            name = "All lists",
            userId = "Default",
            color = "#F9371C",
            colorObject = ListColorList.First()
        };

        public static TaskModel DefaultTask = new TaskModel()
        {
            task = "",
            archived = false,
            dateObject = DateTime.Today,
            listObject = InboxList,
        };

        public static ListModel DefaultList = new ListModel()
        {
            name = "",
            colorObject = ListColorList.First()
        };
    }
}
