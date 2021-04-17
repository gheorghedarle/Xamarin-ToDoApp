using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskDialog : Frame
    {
        public AddTaskDialog()
        {
            InitializeComponent();
        }
    }
}