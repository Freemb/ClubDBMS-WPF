using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Views;
using System.Data;
using DataLibrary.Operations;
using System.Windows.Input;
using WPFUI.Utility;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;

namespace WPFUI.ViewModels
{
	public sealed class ShellViewModel : ObservableObject
	{
		public MembersViewModel MemVM { get; private set; } 
		public VisitsViewModel VisVM { get; private set; }
		public PortalViewModel Portal { get; private set; } 
		public static DataSet Softcache { get; set; }
		private static readonly ShellViewModel _instance; // don't initialise here, calls ctor before static ctor finalises.
        private Visibility _isMenuCollapsed = Visibility.Collapsed; //0-visible, 1-hidden, 2- collapsed
        private object _currentView;
		public object CurrentView
		{
			get { return _currentView; }
			set { OnPropertyChanged(ref _currentView, value); }
		}
		public ICommand LoadPortalCommand { get; private set; }
		public ICommand QuitCommand { get; private set; }
        public ICommand CollapsePaneCommand { get; private set; }
		public static ShellViewModel GetInstance { get { return _instance; } }
        

        public Visibility  IsMenuCollapsed
        {
            get { return _isMenuCollapsed; }
            set { OnPropertyChanged(ref _isMenuCollapsed, value); }
        }

        //static constructor called first
        static ShellViewModel()
		{
			
			Softcache = new DataSet("SoftCache");
			MemberConnector mc1 = new MemberConnector();
			Softcache.Tables.Add(mc1.Load(null));
			ActivityConnector ac2 = new ActivityConnector();
			Softcache.Tables.Add(ac2.Load(null));
			_instance = new ShellViewModel(); //call last in static ctor, as ShellVM needs static softcache
		}
		//instance (singleton) constructor// passing instance calls constructor again before it completes, within itself.**bad***
		private ShellViewModel()
		{
			MemVM = new MembersViewModel();
			VisVM = new VisitsViewModel();
			Portal = new PortalViewModel();
			LoadPortalCommand = new RelayCommand(() => CurrentView = Portal);
			QuitCommand = new RelayCommand(Quit);
            CollapsePaneCommand = new RelayCommand(CollapsePane);
			Portal.LoadMembersCommand = new RelayCommand(() => CurrentView = MemVM);
			Portal.LoadVisitsCommand = new RelayCommand(() => CurrentView = VisVM);
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
            IsMenuCollapsed = IsMenuCollapsed == Visibility.Collapsed?Visibility.Visible:Visibility.Collapsed;
           
        }
		
	}
}
