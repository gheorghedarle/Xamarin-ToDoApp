using Prism.Events;
using Prism.Navigation;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoApp.Events;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AuthPageViewModel : 
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IRegionManager _regionManager { get; }
        private SwitchViewEvent _switchViewEvent;

        #endregion

        #region Properties

        public ObservableCollection<string> AuthScreenList { get; set; }
        public string CurrentAuthScreen { get; set; }

        #endregion

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SwitchToLoginCommand { get; set; }
        public ICommand SwitchToSignUpCommand { get; set; }

        #endregion

        #region Constructors

        public AuthPageViewModel(
            INavigationService navigationService,
            IRegionManager regionManager,
            IEventAggregator eventAggregator) : base(navigationService)
        {
            _regionManager = regionManager;
            
            BackCommand = new Command(BackCommandHandler);
            SwitchToLoginCommand = new Command(SwitchToLoginCommandHandler);
            SwitchToSignUpCommand = new Command(SwitchToSignUpCommandHandler);

            _switchViewEvent = eventAggregator.GetEvent<SwitchViewEvent>();
            _switchViewEvent.Subscribe(SwitchViewEventHandler);
        }

        public void Initialize(INavigationParameters parameters)
        {
            CurrentAuthScreen = "login";
            Title = "Login";

            _regionManager.RequestNavigate("LoginRegion", "LoginTemplate");
            _regionManager.RequestNavigate("SignUpRegion", "SignUpTemplate");
        }

        #endregion

        #region Command Handlers

        private void SwitchToLoginCommandHandler()
        {
            CurrentAuthScreen = "login";
            Title = "Login";
        }

        private void SwitchToSignUpCommandHandler()
        {
            CurrentAuthScreen = "signup";
            Title = "Sign Up";
        }

        private void SwitchViewEventHandler(string view)
        {
            if(view == "Login")
            {
                CurrentAuthScreen = "login";
                Title = "Login";
            }
            else if (view == "SignUp")
            {
                CurrentAuthScreen = "signup";
                Title = "Sign Up";
            }
        }

        #endregion
    }
}
