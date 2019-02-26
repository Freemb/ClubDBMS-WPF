using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DataLibrary.Models;
using DataLibrary.Operations;
using WPFUI.Views;
using WPFUI.Utility;
using System.Windows.Input;
using DataLibrary.Extensions;

namespace WPFUI.ViewModels
{
	public class VisitsViewModel:ObservableObject
	{
		private ObservableCollection<VisitModel> _visits;
		private VisitModel _selectedvisit = new VisitModel();
		private MemberModel _selectedmember = new MemberModel();
		private ObservableCollection<string> _activityList;
		private ObservableCollection<ActivityModel> _allActivityWithprices;

		public ObservableCollection<ActivityModel> AllPrices
		{
			get { return _allActivityWithprices; }
			set { _allActivityWithprices = value; }
		}

		//private ObservableCollection<string> _subactivityList;

		public ObservableCollection<string> SubActivityList
		{
			get { return GetSubActivities(SelectedVisit.Activity.ActivityName); }
			//set { OnPropertyChanged(ref _subactivityList, value); }
		}


		//public ShellViewModel ShellPassed { get; private set; }

		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set { OnPropertyChanged(ref _selectedmember, value); }   
			
		}
		public ObservableCollection<VisitModel> Visits
		{
			get { return _visits; }
			set { OnPropertyChanged(ref _visits, value); } //NotifyOfPropertyChange(() => Visits);
		}
		public ObservableCollection<string> ActivityList
		{
			get { return _activityList; }
			set
			{
				OnPropertyChanged(ref _activityList, value);
			}

		}

		public VisitModel SelectedVisit
		{
			get { return _selectedvisit; }
			set
			{
				OnPropertyChanged(ref _selectedvisit, value);				
				OnPropertyChanged("SubActivityList");
				SelectedMember = ShellViewModel.Softcache.Tables["Members"].GetMemberDetails(_selectedvisit.Member.MemNo);
						
			}
		}
		//Constructor
		public VisitsViewModel()
		{
			VisitConnector vconn = new VisitConnector();
			Visits = new ObservableCollection<VisitModel>(vconn.Load("22/07/2017", true));
			SelectedVisit = Visits.First<VisitModel>();
			ActivityList = GetActivityList();
			AllPrices = new ObservableCollection<ActivityModel>(ShellViewModel.Softcache.Tables["Activities"].ToActivityModelIEnum());
			
		}
		//move elsewhere :extension methods
		private ObservableCollection<string> GetActivityList()
		{
			return new ObservableCollection<string>(ShellViewModel.Softcache.Tables["Activities"].AsEnumerable().Select(
							datarow => datarow.Field<string>("Activity")).Distinct().ToList());

		}

		private ObservableCollection<string> GetSubActivities(string activityname)
		{
			return new ObservableCollection<string>( (from datarow in ShellViewModel.Softcache.Tables["Activities"].AsEnumerable()
					where datarow.Field<string>("Activity") == activityname
					select datarow.Field<string>("SubActivity")).Distinct().ToList());

		}

		
	}
}
