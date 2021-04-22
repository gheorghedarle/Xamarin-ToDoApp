using System.Collections.ObjectModel;

namespace ToDoApp.Helpers
{
    public static class MenuHelper
    {
        public static ObservableCollection<string> AddOptions = new ObservableCollection<string>() {
            "Add a task",
            "Add a project"
        };
    }
}
