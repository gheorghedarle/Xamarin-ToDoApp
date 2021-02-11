using ToDoApp.Views;
using Xamarin.Forms;

namespace ToDoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Sharpnado.HorizontalListView.Initializer.Initialize(true, false);
            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
