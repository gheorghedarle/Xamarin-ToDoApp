using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views.Fragments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZeroStateView : StackLayout
    {
        public ZeroStateView()
        {
            InitializeComponent();
        }
    }
}