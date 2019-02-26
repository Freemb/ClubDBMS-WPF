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

namespace WPFUI.ViewModels
{
	public class VisitsViewModel : ObservableObject
	{
		private ObservableCollection<VisitModel> _visits;
		private VisitModel _selectedvisit = new VisitModel();
		private MemberModel _selectedmember = new MemberModel();
		private ObservableCollection<ActivityModel> _allActivityWithprices;
		//private ObservableCollection<ActivityModel> _activitydetails;
		private IEnumerable<string> _activitylist;
		public ObservableCollection<ActivityModel> AllActivityPrices
		{
			get { return _allActivityWithprices; }
			set { _allActivityWithprices = value; }
		}

		public IEnumerable<string> ActivityList
		{ get
			{
				if(_activitylist == null) return GetActivityList();
				return _activitylist;
			}
			set
			{
				_activitylist = value;
			}
		}
		

		public ObservableCollection<ActivityModel> ActivityDetails
		{
			get { return GetActivityDetails(SelectedVisit.Activity.ActivityName, SelectedVisit.VisitDate); }
			//set { OnPropertyChanged(ref _activitydetails, value); }
		}
		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set
			{
				OnPropertyChanged(ref _selectedmember, value);
			}   
			
		}
		public ObservableCollection<VisitModel> Visits
		{
			get { return _visits; }
			set { OnPropertyChanged(ref _visits, value); } 
		}
		

		public VisitModel SelectedVisit
		{
			get { return _selectedvisit; }
			set
			{
				OnPropertyChanged(ref _selectedvisit, value);				
				OnPropertyChanged("ActivityDetails");
				SelectedMember = ShellViewModel.Softcache.Tables["Members"].GetMemberDetails(_selectedvisit.Member.MemNo);

			}
		}

		//Constructor
		public VisitsViewModel()
		{
			VisitConnector vconn = new VisitConnector();
			Visits = new ObservableCollection<VisitModel>(vconn.Load("22/07/2017", true));
			SelectedVisit = Visits.First<VisitModel>();
			AllActivityPrices = new ObservableCollection<ActivityModel>(ShellViewModel.Softcache.Tables["Activities"].ToActivityModelIEnum());
			
		}
		//move elsewhere :extension methods
		private IEnumerable<string> GetActivityList()
		{ return AllActivityPrices.Select(model => model.ActivityName).Distinct();}

		private ObservableCollection<ActivityModel> GetActivityDetails(string activityname, DateTime date)
		{
			return new ObservableCollection<ActivityModel>( AllActivityPrices.Where(model => 
			(model.ActivityName == activityname && model.IsWEBH == date.IsWeekendBankHoliday())));

		}

		
	}
}
