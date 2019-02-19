using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataLibrary.Models;
using DataLibrary.Operations;
using MinervaWPFUI.Views;

namespace MinervaWPFUI.ViewModels
{
	public class VisitsViewModel: Screen
	{
		private VisitModel _selectedvisit;
		private BindableCollection<VisitModel> _visits;
		private MemberModel _member;

		public MemberModel Member
		{
			get { return _member; }
			set	{_member = value;	NotifyOfPropertyChange(); }
		}


		public BindableCollection<VisitModel> Visits
		{
			get { return _visits; }
			set { _visits = value; NotifyOfPropertyChange(() => Visits); }
		}


		public VisitModel SelectedVisit
		{
			get { return _selectedvisit; }
			set { _selectedvisit = value; }
		}
		public VisitsViewModel()
		{
			VisitConnector vconn = new VisitConnector();
			Visits = new BindableCollection<VisitModel>( vconn.Load("22/07/2017", true));
			
		}

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
