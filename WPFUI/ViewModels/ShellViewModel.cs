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

namespace WPFUI.ViewModels
{
	public sealed class ShellViewModel : ObservableObject
	{
		public MembersViewModel MemVM { get; private set; }
		public VisitsViewModel VisVM { get; private set; }
		public PortalViewModel Portal { get; private set; }
		public static ShellViewModel GetInstance { get { return _instance; } }
		public static DataSet Softcache { get; set; } = new DataSet();
				
		private static readonly ShellViewModel _instance = new ShellViewModel();
		private object _currentView;
		public object CurrentView
		{
			get { return _currentView; }
			set { OnPropertyChanged(ref _currentView, value); }
		}

		public ICommand LoadMembersCommand { get; private set; }
		public ICommand LoadVisitsCommand { get; private set; }
		public ICommand LoadPortalCommand { get; private set; }
		public ICommand QuitCommand { get; private set; }

		private void Quit()
		{
			if (MessageBox.Show("Are you sure you want to exit?","Quitting the Application ....",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				Application.Current.MainWindow.Close();
			}
		}

		private ShellViewModel()

		{
			MemberConnector mc1 = new MemberConnector();
			Softcache.Tables.Add(mc1.Load(null));
			ActivityConnector ac2 = new ActivityConnector();
			Softcache.Tables.Add(ac2.Load(null));

			Portal = new PortalViewModel(this); VisVM = new VisitsViewModel(this); MemVM = new MembersViewModel(this); //passing instance of shellviewmodel to portal for access to commands
			LoadMembersCommand = new RelayCommand(() => CurrentView = MemVM);
			LoadVisitsCommand = new RelayCommand(() => CurrentView = VisVM);
			LoadPortalCommand = new RelayCommand(() => CurrentView = Portal);
			QuitCommand = new RelayCommand(Quit);
			CurrentView = Portal; //Loads portal on opening


		}
	}
}
