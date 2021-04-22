using System.ComponentModel;

namespace ToDoApp.Models
{
    public class BaseModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
