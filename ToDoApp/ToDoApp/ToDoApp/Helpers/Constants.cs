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
            new ColorModel() { Name="Red", Color = "#F9371C" },
            new ColorModel() { Name="Orange", Color = "#F97C1C" },
            new ColorModel() { Name="Yellow", Color = "#F9C81C" },
            new ColorModel() { Name="Turquoise", Color = "#41D0B6" },
            new ColorModel() { Name="Cyan", Color = "#2CADF6" },
            new ColorModel() { Name="Purple", Color = "#6562FC" },
        };

        public static ListModel InboxList = new ListModel() {
            Id = "zlDZNn3sNmyirSNs3mRY",
            Name = "Inbox",
            UserId = "Default",
            Color = "#F9371C"
        };

        public static ListModel AllLists = new ListModel()
        {
            Id = "alllist",
            Name = "All lists",
            UserId = "Default",
            Color = "#F9371C",
        };

        public static TaskModel DefaultTask = new TaskModel()
        {
            Task = "",
            Archived = false,
            List = "Inbox",
            Date = DateTime.Now.ToString("dd/MM/yyyy")
        };

        public static ListModel DefaultList = new ListModel()
        {
            Name = "",
            Color = "#F9371C",
        };
    }
}
