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

namespace WPFUI.ViewModels
{
	public class VisitsViewModel:ObservableObject
	{
		private VisitModel _selectedvisit;
		private ObservableCollection<VisitModel> _visits;
		private MemberModel _member;

		public MemberModel Member
		{
			get { return _member; }
			set { OnPropertyChanged(ref _member, value); }   //NotifyOfPropertyChange(); 
			
		}


		public ObservableCollection<VisitModel> Visits
		{
			get { return _visits; }
			set { OnPropertyChanged(ref _visits, value); } //NotifyOfPropertyChange(() => Visits);
			
		}


		public VisitModel SelectedVisit
		{
			get { return _selectedvisit; }
			set { OnPropertyChanged(ref _selectedvisit, value); }
		}
		public VisitsViewModel()
		{
			VisitConnector vconn = new VisitConnector();
			Visits = new ObservableCollection<VisitModel>( vconn.Load("22/07/2017", true));
			
		}

		//move elsewhere

		public List<string> LoadActivities()
		{
			return ShellView.Softcache.Tables["Activities"].AsEnumerable().Select(
									datarow => datarow.Field<string>("Activity")).Distinct().ToList();
		}
		public List<string> FilterSubActivities()
		{
			return (from datarow in ShellView.Softcache.Tables["Activities"].AsEnumerable()
			 where datarow.Field<string>("Activity") == "selected activity here"
			 select datarow.Field<string>("SubActivity")).Distinct().ToList();
		}


	}
}
