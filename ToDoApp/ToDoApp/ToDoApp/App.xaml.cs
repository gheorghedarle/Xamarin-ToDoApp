using ToDoApp.Views;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome-Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome-Solid")]

[assembly: ExportFont("Cairo-Bold.ttf", Alias = "CairoBold")]
[assembly: ExportFont("Cairo-SemiBold.ttf", Alias = "CairoSemiBold")]
[assembly: ExportFont("Cairo-Regular.ttf", Alias = "CairoRegular")]
[assembly: ExportFont("Cairo-Light.ttf", Alias = "CairoLight")]

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
