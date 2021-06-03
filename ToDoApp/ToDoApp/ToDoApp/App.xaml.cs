using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using ToDoApp.Auth;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Services.DateService;
using ToDoApp.ViewModels;
using ToDoApp.ViewModels.Dialogs;
using ToDoApp.Views;
using ToDoApp.Views.Dialogs;
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
        { }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var isLoggedIn = auth.IsLoggedIn();
            if(isLoggedIn)
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
            containerRegistry.Register<IFirestoreRepository<ProjectModel>, ProjesctsRepository>();

            containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>("WelcomePage");
            containerRegistry.RegisterForNavigation<TasksPage, TasksPageViewModel>("TasksPage");
            containerRegistry.RegisterForNavigation<AddTaskPage, AddTaskPageViewModel>("AddTaskPage");
            containerRegistry.RegisterForNavigation<AddProjectPage, AddProjectPageViewModel>("AddProjectPage");
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>("ProfilePage");

            containerRegistry.RegisterDialog<AddDialog, AddDialogViewModel>();
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
