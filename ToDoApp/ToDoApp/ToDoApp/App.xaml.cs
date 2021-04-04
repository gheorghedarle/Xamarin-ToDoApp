using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using ToDoApp.Services.DateService;
using ToDoApp.ViewModels;
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
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) 
        { 
            Sharpnado.HorizontalListView.Initializer.Initialize(true, false);
        }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomePage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDateService, DateService>();

            containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>("WelcomePage");
            containerRegistry.RegisterForNavigation<TasksPage, TasksPageViewModel>("TasksPage");
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
