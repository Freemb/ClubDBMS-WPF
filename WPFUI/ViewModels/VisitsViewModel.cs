﻿using System;
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
using System.Windows.Controls;

namespace WPFUI.ViewModels
{
	public class VisitsViewModel : ObservableObject
	{
		private ObservableCollection<VisitModel> _visits;
		private VisitModel _selectedvisit = new VisitModel();
		private MemberModel _selectedmember = new MemberModel();
		private ObservableCollection<string> _activitylist;
		private ObservableCollection<string> _subactivitylist;

		public IEnumerable<ActivityModel> AllActivityPrices;
		public ICommand GetSubActivityListCommand { get; set; }

		public ObservableCollection<string> ActivityList
		{
			get
			{
				if(_activitylist == null) return GetActivityList();
				return _activitylist;
			}
			set
			{
				_activitylist = value;
			}
		}
		

		public ObservableCollection<string> SubActivityList
		{
			get
			{

				if (_subactivitylist == null) GetSubActivityList();
				return _subactivitylist;
			}
				
			set
			{
				OnPropertyChanged(ref _subactivitylist, value); 
			}
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
				SelectedMember = ShellViewModel.Softcache.Tables["Members"].GetMemberDetails(_selectedvisit.Member.MemNo);
			}
		}

		//Constructor
		public VisitsViewModel()
		{
			VisitConnector vconn = new VisitConnector();
			Visits = new ObservableCollection<VisitModel>(vconn.Load("22/07/2017", true));
			SelectedVisit = Visits.First<VisitModel>();
			AllActivityPrices = ShellViewModel.Softcache.Tables["Activities"].ToActivityModelIEnum();
			GetSubActivityListCommand = new RelayCommand(GetSubActivityList);

		}
		//move elsewhere :extension methods
		private ObservableCollection<string> GetActivityList()
		{ return new ObservableCollection<string>( AllActivityPrices.Select(model => model.ActivityName).Distinct());}


		private void GetSubActivityList()
		{
			SubActivityList = new ObservableCollection<string>(AllActivityPrices.Where(model =>
			model.ActivityName == SelectedVisit.Activity.ActivityName && model.IsWEBH == SelectedVisit.VisitDate.IsWeekendBankHoliday()).Select(model => model.SubActivity));
			
		}
		private ObservableCollection<decimal> GetPrice(string activityname, DateTime date)
		{
			return new ObservableCollection<decimal>(AllActivityPrices.Where(model =>
			model.ActivityName == activityname && model.IsWEBH == date.IsWeekendBankHoliday()).Select(model => model.Price));

		}
		
		
	}
}
