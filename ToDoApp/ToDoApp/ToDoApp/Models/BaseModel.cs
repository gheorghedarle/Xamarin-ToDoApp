using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ToDoApp.Models
{
    public class BaseModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
