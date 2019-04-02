using DataLibrary.Cache;
using DataLibrary.Operations;
using System.Windows;
using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public sealed class ShellViewModel : ObservableObject
	{
        public PortalViewModel Portal { get; private set; }
        private static readonly ShellViewModel _instance; // don't initialise here, calls ctor before static ctor finalises.
        private Visibility _isMenuCollapsed = Visibility.Collapsed; //0-visible, 1-hidden, 2- collapsed
        private object _currentView;
        //implement stack for back navigation
        public object CurrentView
		{
			get { return _currentView; }
			set { OnPropertyChanged(ref _currentView, value); }
		}
        
		public ICommand LoadPortalCommand { get; private set; }
		public ICommand QuitCommand { get; private set; }
        public ICommand CollapsePaneCommand { get; private set; }

        public static ShellViewModel GetInstance { get => _instance; } 
        

        public Visibility  IsMenuCollapsed
        {
            get { return _isMenuCollapsed; }
            set { OnPropertyChanged(ref _isMenuCollapsed, value); }
        }

        //static constructor called first
        static ShellViewModel()
		{
			MemberConnector mc1 = new MemberConnector();
            ActivityConnector ac2 = new ActivityConnector();
            CacheOps.GetFromCache("Members", mc1);
            CacheOps.GetFromCache("Activities", ac2);
            _instance = new ShellViewModel(); //call last in static ctor, as ShellVM needs static softcache
		}
		//instance (singleton) constructor
		private ShellViewModel()
		{
            //Shell Objects
            Portal = new PortalViewModel();
            LoadPortalCommand = new RelayCommand(() => CurrentView = Portal,()=>true);
			QuitCommand = new RelayCommand(Quit,()=>true);
            CollapsePaneCommand = new RelayCommand(CollapsePane,()=>true);
            //Portal Objects
            Portal.LoadMembersPortalCommand = new RelayCommand(() => CurrentView = Portal.MemPortalVM, () => true);
            Portal.LoadEventsPortalCommand = new RelayCommand(() => CurrentView = Portal.EventPortalVM, () => true);
            Portal.LoadDeliveriesCommand = new RelayCommand(() => CurrentView = Portal.DelVM, () => true);
            //Portal Sub-Objects
            Portal.MemPortalVM.LoadMembersCommand = new RelayCommand(() => CurrentView = Portal.MemPortalVM.MemVM,()=>true);
			Portal.MemPortalVM.LoadVisitsCommand = new RelayCommand(() => CurrentView = Portal.MemPortalVM.VisVM,()=>true);
            Portal.EventPortalVM.LoadEventsCommand = new RelayCommand(() => CurrentView = Portal.EventPortalVM.EventVM, () => true);
            
            CurrentView = Portal; //Loads portal on opening

		}
		private void Quit()
		{
			if (MessageBox.Show("Are you sure you want to exit?", "Quitting the Application ....", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				Application.Current.MainWindow.Close();
			}
		}
        private void CollapsePane()
        {
            IsMenuCollapsed = IsMenuCollapsed == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

        }


    }
}
