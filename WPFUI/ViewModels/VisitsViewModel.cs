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
		private VisitModel _selectedvisit;
		private ObservableCollection<VisitModel> _visits;
		private MemberModel _selectedmember;
		private ObservableCollection<string> _activityList;

		public ShellViewModel ShellDerived { get; private set; }

		public ICommand GetActivityListCommand;

		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set { OnPropertyChanged(ref _selectedmember, value); }   //NotifyOfPropertyChange(); 
			
		}


		public ObservableCollection<VisitModel> Visits
		{
			get { return _visits; }
			set { OnPropertyChanged(ref _visits, value); } //NotifyOfPropertyChange(() => Visits);
		}
		public ObservableCollection<string> ActivityList
		{
			get { return _activityList; }
			set { OnPropertyChanged(ref _activityList, value); }

		}

		public VisitModel SelectedVisit
		{
			get { return _selectedvisit; }
			set
			{
				OnPropertyChanged(ref _selectedvisit, value);
				SelectedMember = GetMemberDetails(_selectedvisit.Member.MemNo);
				
			}
		}
		public VisitsViewModel():this(ShellViewModel.GetInstance)
		{
			
		}
		public VisitsViewModel(ShellViewModel shellderived)
		{
			ShellDerived = shellderived;
			VisitConnector vconn = new VisitConnector();
			ActivityConnector aconn = new ActivityConnector();
			Visits = new ObservableCollection<VisitModel>(vconn.Load("22/07/2017", true));
			GetActivityList(aconn);

		}

		private void GetActivityList(ActivityConnector aconn)
		{
			ActivityList = new ObservableCollection<string>(ShellViewModel.Softcache.Tables["Activities"].AsEnumerable().Select(
							datarow => datarow.Field<string>("Activity")).Distinct().ToList());

			//GetActivityListCommand = new RelayCommand(GetActivityList);
			//ActivityList = new ObservableCollection<ActivityModel>(aconn.Load(true));
		}


		//move elsewhere
		public MemberModel GetMemberDetails(double memno)
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
		
		
		
		public List<string> GetSubActivities()
		{
			return (from datarow in ShellViewModel.Softcache.Tables["Activities"].AsEnumerable()
					where datarow.Field<string>("Activity") == "selected activity here"
					select datarow.Field<string>("SubActivity")).Distinct().ToList();

		}


	}
}
