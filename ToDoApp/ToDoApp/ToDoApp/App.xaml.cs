using ToDoApp.Views;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]

[assembly: ExportFont("Cairo-Bold.ttf", Alias = "Cairo_Bold")]
[assembly: ExportFont("Cairo-SemiBold.ttf", Alias = "Cairo_SemiBold")]
[assembly: ExportFont("Cairo-Regular.ttf", Alias = "Cairo_Regular")]
[assembly: ExportFont("Cairo-Light.ttf", Alias = "Cairo_Light")]

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
