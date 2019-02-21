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

namespace WPFUI.ViewModels
{
	public class ShellViewModel:ObservableObject
	{
		public MembersViewModel MemVM { get; private set; }
		public VisitsViewModel VisVM { get; private set; }
		private object _currentView;
		public object CurrentView
		{
			get { return _currentView; }
			set {OnPropertyChanged(ref _currentView, value); }
		}
			   
		public ICommand LoadMembersCommand { get; private set; }
		public ICommand LoadVisitsCommand { get; private set; }
		public void LoadMembers()
		{
			CurrentView = MemVM;
		}
		public void LoadVisits()
		{
			CurrentView = VisVM;
		}
		public ShellViewModel()
		{
			MemVM = new MembersViewModel();
			VisVM = new VisitsViewModel();
			LoadMembersCommand = new RelayCommand(LoadMembers);
			LoadVisitsCommand = new RelayCommand(LoadVisits);

			//MemberConnector mc1 = new MemberConnector();
			//ShellView.Softcache.Tables.Add(mc1.Load(null));
			//ActivityConnector ac2 = new ActivityConnector();
			//ShellView.Softcache.Tables.Add(ac2.Load(null));
		}
	}
}
