using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views.Templates.AddItem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskTemplate : StackLayout
    {
        public AddTaskTemplate()
        {
            InitializeComponent();
        }
    }
}