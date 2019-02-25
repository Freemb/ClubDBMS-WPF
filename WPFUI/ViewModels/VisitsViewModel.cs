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
	public class VisitsViewModel:ObservableObject
	{
		private ObservableCollection<VisitModel> _visits;
		private VisitModel _selectedvisit = new VisitModel();
		private MemberModel _selectedmember = new MemberModel();
		private ObservableCollection<string> _activityList;
		private IEnumerable<ActivityModel> _allActivityWithprices;

		public IEnumerable<ActivityModel> AllPrices
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


		public ShellViewModel ShellPassed { get; private set; }

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
				SelectedMember = GetMemberDetails(_selectedvisit.Member.MemNo);
						
			}
		}
		//Constructors
		public VisitsViewModel()//this(ShellViewModel.GetInstance)
		{
			
		}
		public VisitsViewModel(ShellViewModel shell)
		{
			this.ShellPassed = shell;
			VisitConnector vconn = new VisitConnector();
			ActivityConnector aconn = new ActivityConnector();
			Visits = new ObservableCollection<VisitModel>(vconn.Load("22/07/2017", true));
			SelectedVisit = Visits.First<VisitModel>();
			ActivityList = GetActivityList();
			//AllPrices = Convertactivity();
		}

		


		//move elsewhere :extension methods
		private MemberModel GetMemberDetails(double memno)
		{
			DataRow dr = ShellViewModel.Softcache.Tables["Members"].AsEnumerable().Where(datarow => datarow.Field<double>("MemNo") == memno).Single();
			return new MemberModel
			{
				MemNo = dr.Field<double>("MemNo"),
				Forename = dr.Field<string>("Forename"),
				Surname = dr.Field<string>("Surname"),
				Category = dr.Field<string>("Category"),
				Email = dr.Field<string>("Email"),
				MobileTel = dr.Field<string>("MobileTel"),
				HomeTel = dr.Field<string>("HomeTel"),
				Gender = dr.Field<string>("Gender"),
				DateOfBirth = dr.IsNull("DOB") ? DateTime.MinValue : dr.Field<DateTime>("DOB")
			};
		}

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

		private IEnumerable<ActivityModel> Convertactivity()
		{
			ActivityConnector ac2 = new ActivityConnector();
			DataTable table = ac2.Load(null);
			
			IEnumerable<DataRow> dt = table.AsEnumerable();
			IEnumerable<ActivityModel> output = dt.Select(row => new ActivityModel()
			{
				SubActivityID = row.Field<int>("SubActivityId"),
				ActivityID = row.Field<int>("ActivityId"),
				ActivityName = row.Field<string>("Activity"),
				SubActivity = row.Field<string>("SubActivity"),
				ActivityType = row.Field<string>("Type"),
				Price = row.Field<decimal>("Price"),
				IsWEBH = row.Field<bool>("WkBH")});
			return output;
		}
	}
}
