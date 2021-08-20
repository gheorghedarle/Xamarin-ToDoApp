using Prism.Navigation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class ProfilePageViewModel : 
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IFirestoreRepository<TaskModel> _taskRepository;
        private IFirestoreRepository<ListModel> _listRepository;

        #endregion

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand DarkModeToggleCommand { get; set; }
        public ICommand HideDoneToggleCommand { get; set; }

        #endregion

        #region Properties

        public ProfileDetailsModel ProfileDetails { get; set; }
        public string Username { get; set; }
        public bool IsDarkMode { get; set; }
        public bool HideDoneTasks { get; set; }

        #endregion

        #region Constructors

        public ProfilePageViewModel(
            INavigationService navigationService,
            IFirestoreRepository<TaskModel> taskRepository,
            IFirestoreRepository<ListModel> listRepository) : base(navigationService)
        {
            _taskRepository = taskRepository;
            _listRepository = listRepository;

            BackCommand = new Command(BackCommandHandler);
            LogOutCommand = new Command(LogOutCommandHandler);
            DarkModeToggleCommand = new Command(DarkModeToggleCommandHandler);
            HideDoneToggleCommand = new Command(HideDoneToggleCommandHandler);
        }

        public async void Initialize(INavigationParameters parameters)
        {
            MainState = LayoutState.Loading;

            await GetProfileDetails();

            IsDarkMode = Application.Current.UserAppTheme.Equals(OSAppTheme.Dark);
            HideDoneTasks = Preferences.Get("hideDoneTasks", false);

            var auth = DependencyService.Get<IFirebaseAuthentication>();
            Username = auth.GetUsername();

            MainState = LayoutState.None;
        }

        #endregion

        #region Command Handlers

        private void DarkModeToggleCommandHandler()
        {
            if (IsDarkMode)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("theme", "dark");
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("theme", "light");
            }
        }

        private void HideDoneToggleCommandHandler()
        {
            HideDoneTasks = !HideDoneTasks;
            Preferences.Set("hideDoneTasks", HideDoneTasks);
        }

        private void LogOutCommandHandler()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var response = auth.LogOut();
            if(response)
            {
                _navigationService.NavigateAsync($"/{nameof(WelcomePage)}");
            }
            else
            {
                Debug.WriteLine("Failed to log out");
            }
        }

        #endregion

        #region Private Methods

        private async Task GetProfileDetails()
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            var userId = auth.GetUserId();
            var lists = await _listRepository.GetAll(userId).GetAsync();
            var tasks = await _taskRepository.GetAll(userId).GetAsync();

            ProfileDetails = new ProfileDetailsModel()
            {
                TotalLists = lists.Count,
                TotalTasks = tasks.Count,
                DoneTasks = tasks.ToObjects<TaskModel>().Count(t => t.archived == true)
            };
        }

        #endregion
    }
}
