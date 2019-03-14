using DataLibrary.Cache;
using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class VisitsViewModel : ObservableObject
	{
        //private objects
        private bool _isReadOnly = true;
        private VisitModel _selectedvisit = new VisitModel();
        private VisitModel dirtySelection;
        private MemberModel _selectedmember = new MemberModel();
        private ObservableCollection<VisitModel> _sourceModels;
		private ObservableCollection<string> _activitylist;
		private ObservableCollection<string> _subactivitylist;
		private IEnumerable<ActivityModel> _activitiesWithPrices;
       
        #region ICommands for binding to buttons/events
        public ICommand GetSubActivityListCommand { get; private set; }
		public ICommand GetPriceCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand GetMemberDetailsCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }  
        public ICommand NextCommand { get; private set; }
        public ICommand LastCommand { get;private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }  
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloneCommand { get; private set; }
        public ICommand CancelCommand { get;private set; }
        #endregion

        //properties for binding to View control properties
        public ObservableCollection<VisitModel> SourceModels
        {
            get { return _sourceModels; }
            set { OnPropertyChanged(ref _sourceModels, value); }
        }
        public VisitModel SelectedModel
        {
            get { return _selectedvisit; }
            set
            {
                OnPropertyChanged(ref _selectedvisit, value);

            }
        }
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
		
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                OnPropertyChanged(ref _isReadOnly , value);
                OnPropertyChanged("IsEditMode");
            }
            
        }
        public bool IsEditMode

        {
            get { return !_isReadOnly; }
          
        }
        
        //Constructor
        public VisitsViewModel()
		{
            //Load Visits and Activity Prices from database to observable collection
            VisitConnector vconn = new VisitConnector();
			SourceModels = new ObservableCollection<VisitModel>(vconn.Load(null,true));
			SelectedModel = SourceModels.FirstOrDefault();
			_activitiesWithPrices = CacheOps.GetFromCache<ActivityModel>("Activities");
           
            #region Set Command Delegates to methods
            GetSubActivityListCommand = new RelayCommand(GetSubActivityList,()=>true);
			GetPriceCommand = new RelayCommand(GetPrice,()=>true);
            GetMemberDetailsCommand = new RelayCommand(GetMemberDetails, () => true);
            LastCommand = new RelayCommand(Last,()=>IsReadOnly);
            FirstCommand = new RelayCommand(First, () => IsReadOnly);
            PreviousCommand = new RelayCommand(Previous, () => IsReadOnly);
            NextCommand = new RelayCommand(Next, () => IsReadOnly);
            AddCommand = new RelayCommand(Add, () => IsReadOnly);
            SaveCommand = new RelayCommand(Save, () => IsEditMode);
            EditCommand = new RelayCommand(Edit, () => IsReadOnly);
            DeleteCommand = new RelayCommand(Delete, () => IsReadOnly);
            CloneCommand = new RelayCommand(Clone, () => IsReadOnly);
            CancelCommand = new RelayCommand(Cancel, () => IsEditMode);
            #endregion
        }

        //Fetch Methods for data bound to view controls by System.Windows.Interactivity triggers
        private ObservableCollection<string> GetActivityList()
		{ return  new ObservableCollection<string>( _activitiesWithPrices.Select(model => model.ActivityName).Distinct());}
		private void GetSubActivityList()
		{
            if (SelectedModel != null )
            {
                SubActivityList = new ObservableCollection<string>(_activitiesWithPrices.Where(model =>
                model.ActivityName == SelectedModel.Activity.ActivityName).Select(model => model.SubActivity).Distinct());
            }
		}
		private void GetPrice()
		{
            // This code returns exception if no match found in activities
            if (!(String.IsNullOrEmpty(SelectedModel?.Activity.ActivityName) || String.IsNullOrEmpty(SelectedModel?.Activity.SubActivity)))
                SelectedModel.Amount = _activitiesWithPrices.Where(model => model.ActivityName == SelectedModel.Activity.ActivityName &&
                model.SubActivity == SelectedModel.Activity.SubActivity && model.IsWEBH == SelectedModel.VisitDate.IsWeekendBankHoliday()
                ).Select(model => model.Price).FirstOrDefault();
        }
        private void GetMemberDetails()
        {
            if(SelectedModel != null)
            SelectedModel.Member = CacheOps.GetFromCache<MemberModel>("Members").GetMemberDetails(_selectedvisit.Member.MemNo);
        }

        #region Navigation Bar Methods
        private void First()
        {
            VisitModel temp = SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            VisitModel temp = SourceModels?.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            int index = GetIndex(SelectedModel.ID);
            VisitModel temp = index - 1 > 0 ? SourceModels?[index - 1] : SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Next()
        {
            int index = GetIndex(SelectedModel.ID);
            VisitModel temp = index + 1 < SourceModels.IndexOf(SourceModels.LastOrDefault()) ? SourceModels[index + 1] : SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Add()
        {
            SourceModels?.Add(new VisitModel());
            SelectedModel = SourceModels?.Last();
            SubActivityList = null;
            ActivityList = null;
            IsReadOnly = false;
        }
        private void Edit() 
        {
            IsReadOnly = false;
            dirtySelection = SelectedModel?.Clone(SelectedModel);       
        }
        private void Save()
        {
            //implement validation checks before saving
            try
            {
                VisitConnector vconn = new VisitConnector();
                if (SelectedModel?.ID == 0)
                {
                    SelectedModel.ID = vconn.Insert(SelectedModel);
                }
                else { vconn.Update(SelectedModel); }
                if (vconn.Ex == null)
                {
                    MessageBox.Show("Visit Added Successfully");
                }
                else { MessageBox.Show(vconn.Ex.Message); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                dirtySelection = null;
                IsReadOnly = true;
            }
            
            
        }
        private void Delete()
        {
            if(SelectedModel != null)
            {
                if(SelectedModel.ID != 0)
                {
                    VisitConnector vconn = new VisitConnector();
                    vconn.Delete(SelectedModel);
                    if (vconn.Ex == null){MessageBox.Show("Visit Deleted Successfully");}
                    else { MessageBox.Show(vconn.Ex.Message); }
                }
                SourceModels.Remove(SelectedModel);
                SelectedModel = SourceModels.LastOrDefault();
            }
            
        }
        private void Clone()
        {
            if( SelectedModel != null)
            {
                VisitModel temp = SelectedModel;
                Add();
                SelectedModel.VisitDate = temp.VisitDate;
                SelectedModel.Member = temp.Member;
            }
        }
        private void Cancel()
        {
            if(SelectedModel != null)
            {
                //Cancel new visit
                if (SelectedModel.ID == 0)
                {
                    SourceModels.Remove(SourceModels.Last());
                    SelectedModel = SourceModels.LastOrDefault();
                }
                //Cancel Edit Visit
                else if(dirtySelection !=null)
                {
                    SourceModels[GetIndex(SelectedModel.ID)] = dirtySelection;
                    dirtySelection = null;
                }
            }
            IsReadOnly = true;
        }
        #endregion

        private int GetIndex(int visitid)
        {
            VisitModel temp = SourceModels.Where((model) => model.ID == visitid).FirstOrDefault();
            return SourceModels.IndexOf(temp);
        }

    }
}
