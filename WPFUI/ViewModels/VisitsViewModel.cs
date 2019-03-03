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
        //private fields
        private VisitModel _selectedvisit = new VisitModel();
        private MemberModel _selectedmember = new MemberModel();
        private ObservableCollection<VisitModel> _visits;
		private ObservableCollection<string> _activitylist;
		private ObservableCollection<string> _subactivitylist;
		private IEnumerable<ActivityModel> _activitiesWithPrices;
        //Commands for binding to buttons/events
		public ICommand GetSubActivityListCommand { get; private set; }
		public ICommand GetPriceCommand { get; private set; }
        public ICommand AddVisitCommand { get; private set; }
        public ICommand GetMemberDetailsCommand { get; set; }
        //properties for binding to display controls
		public ObservableCollection<string> ActivityList
		{
			get
			{
				if(_activitylist == null) return GetActivityList();
				return _activitylist;
			}
			set
			{
				OnPropertyChanged(ref _activitylist , value);
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
				
			}
		}

		//Constructor
		public VisitsViewModel()
		{
			VisitConnector vconn = new VisitConnector();
			Visits = new ObservableCollection<VisitModel>(vconn.Load("22/07/2017", true));
			SelectedVisit = Visits.First<VisitModel>();
			_activitiesWithPrices = ShellViewModel.Softcache.Tables["Activities"].ToActivityModelIEnum();
			GetSubActivityListCommand = new RelayCommand(GetSubActivityList);
			GetPriceCommand = new RelayCommand(GetPrice);
            AddVisitCommand = new RelayCommand(AddVisit);
            GetMemberDetailsCommand = new RelayCommand(GetMemberDetails);

		}
		
		private ObservableCollection<string> GetActivityList()
		{ return  new ObservableCollection<string>( _activitiesWithPrices.Select(model => model.ActivityName).Distinct());}

		private void GetSubActivityList()
		{
            SubActivityList = new ObservableCollection<string>(_activitiesWithPrices.Where(model =>
			model.ActivityName == SelectedVisit.Activity.ActivityName).Select(model => model.SubActivity).Distinct());
		}
		private void GetPrice()
		{
			//// This code returns exception if no match found in activities
			//if (!(String.IsNullOrEmpty(SelectedVisit.Activity.ActivityName) || String.IsNullOrEmpty(SelectedVisit.Activity.SubActivity)))
			//	SelectedVisit.Amount = _activitiesWithPrices.Where(model => model.ActivityName == SelectedVisit.Activity.ActivityName &&
			//	model.SubActivity == SelectedVisit.Activity.SubActivity && model.IsWEBH == SelectedVisit.VisitDate.IsWeekendBankHoliday()
			//	).Select(model => model.Price).First();

			// This code does the same as above with no exception as there is no assignment unless a match is found
			if (!(String.IsNullOrEmpty(SelectedVisit.Activity.ActivityName) || String.IsNullOrEmpty(SelectedVisit.Activity.SubActivity)))
			foreach (ActivityModel model in _activitiesWithPrices)
			{
				if (model.ActivityName == SelectedVisit.Activity.ActivityName && model.SubActivity == SelectedVisit.Activity.SubActivity
					&& model.IsWEBH == SelectedVisit.VisitDate.IsWeekendBankHoliday())
				{
					SelectedVisit.Amount = model.Price;
					return;
				}

			}
		}
		
        private void AddVisit()
        {
            
            Visits.Add(new VisitModel());
            SelectedVisit = Visits.Last();
            SubActivityList = null;
            ActivityList = null;
            
        }
        private void GetMemberDetails()
        {
            SelectedVisit.Member = ShellViewModel.Softcache.Tables["Members"].GetMemberDetails(_selectedvisit.Member.MemNo);
        }
		
	}
}
