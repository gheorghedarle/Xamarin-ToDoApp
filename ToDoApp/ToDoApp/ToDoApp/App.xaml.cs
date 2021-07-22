using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using ToDoApp.Auth;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Services.DateService;
using ToDoApp.ViewModels;
using ToDoApp.ViewModels.Templates;
using ToDoApp.Views;
using ToDoApp.Views.Templates.AddItem;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]

[assembly: ExportFont("Barlow-Bold.ttf", Alias = "Barlow_Bold")]
[assembly: ExportFont("Barlow-SemiBold.ttf", Alias = "Barlow_SemiBold")]
[assembly: ExportFont("Barlow-Medium.ttf", Alias = "Barlow_Medium")]
[assembly: ExportFont("Barlow-Regular.ttf", Alias = "Barlow_Regular")]
[assembly: ExportFont("Barlow-Light.ttf", Alias = "Barlow_Light")]

namespace ToDoApp
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) 
        { }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var isLoggedIn = auth.IsLoggedIn();
            if (isLoggedIn)
            {
                await NavigationService.NavigateAsync($"/{nameof(TasksPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomePage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDateService, DateService>();
            containerRegistry.Register<IFirestoreRepository<TaskModel>, TasksRepository>();
            containerRegistry.Register<IFirestoreRepository<ListModel>, ListsRepository>();

            containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>("WelcomePage");
            containerRegistry.RegisterForNavigation<TasksPage, TasksPageViewModel>("TasksPage");
            containerRegistry.RegisterForNavigation<AddPage, AddPageViewModel>("AddPage");
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>("ProfilePage");
            containerRegistry.RegisterForNavigation<AuthPage, AuthPageViewModel>("AuthPage");
            containerRegistry.RegisterForNavigation<MorePage, MorePageViewModel>("MorePage");
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
