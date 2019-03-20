using DataLibrary.Cache;
using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class MembersViewModel: ObservableObject
	{
        private bool _isReadOnly = true;
        private MemberModel dirtySelection;
        private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();
		private MemberModel _selectedmember = new MemberModel();
		public ObservableCollection<MemberModel> SourceModels
		{
			get { return _members; }
			set { OnPropertyChanged(ref _members, value); } 
		}
		public MemberModel SelectedModel
		{
			get { return _selectedmember; }
			set { OnPropertyChanged(ref _selectedmember, value); }
		}

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                OnPropertyChanged(ref _isReadOnly, value);
                OnPropertyChanged("IsEditMode");
            }
        }
        public bool IsEditMode{ get => !_isReadOnly; }

        #region ICommands for binding to buttons/events
        public ICommand AddCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        #endregion

        //constructor
        public MembersViewModel()
		{
			SourceModels = new ObservableCollection<MemberModel>(CacheOps.GetFromCache<MemberModel>("Members"));
            SelectedModel = SourceModels.FirstOrDefault();
            #region Set Command Delegates to methods
            LastCommand = new RelayCommand(Last, () => IsReadOnly);
            FirstCommand = new RelayCommand(First, () => IsReadOnly);
            PreviousCommand = new RelayCommand(Previous, () => IsReadOnly);
            NextCommand = new RelayCommand(Next, () => IsReadOnly);
            AddCommand = new RelayCommand(Add, () => IsReadOnly);
            SaveCommand = new RelayCommand(Save, () => IsEditMode);
            EditCommand = new RelayCommand(Edit, () => IsReadOnly);
            DeleteCommand = new RelayCommand(Delete, () => IsReadOnly);
            CancelCommand = new RelayCommand(Cancel, () => IsEditMode);
            #endregion
        }
        #region Navigation Bar Methods
        private void First()
        {
            if (SourceModels == null) return;
            MemberModel temp = SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            if (SourceModels == null) return;
            MemberModel temp = SourceModels?.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            if (SourceModels == null) return;
            int index = SourceModels.GetCollectionIndex(SelectedModel?.ID);
            MemberModel temp = index - 1 > 0 ? SourceModels?[index - 1] : SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Next()
        {
            if (SourceModels == null) return;
            int index = SourceModels.GetCollectionIndex(SelectedModel?.ID);
            MemberModel temp = index + 1 < SourceModels.IndexOf(SourceModels.LastOrDefault()) ? SourceModels[index + 1] : SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Add()
        {
            if (SourceModels == null) return;
            SourceModels?.Add(new MemberModel());
            SelectedModel = SourceModels?.LastOrDefault();
            IsReadOnly = false;
        }
        private void Edit()
        {
            if (SourceModels == null) return;
            IsReadOnly = false;
            dirtySelection = SelectedModel?.Clone(); //not a deep clone, test  
        }
        private void Save()
        {
            if (SourceModels == null) return;
            //implement validation checks before saving
            try
            {
                MemberConnector conn = new MemberConnector();
                if (SelectedModel?.ID == 0)
                {
                    conn.Insert(SelectedModel);
                }
                else { conn.Update(SelectedModel); }
                if (conn.Ex == null)
                {
                    MessageBox.Show("Member Added Successfully");
                }
                else { MessageBox.Show(conn.Ex.Message); }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dirtySelection = null;
                IsReadOnly = true;
            }
        }
        private void Delete()
        {
            if (SelectedModel == null) return;
            MemberConnector conn = new MemberConnector();
            if (SelectedModel.ID == 0 || conn.Delete(SelectedModel))
            {
                SourceModels.Remove(SelectedModel);
                SelectedModel = SourceModels.LastOrDefault();
                MessageBox.Show("Event Deleted Successfully");
                return;
            }

            if (conn.Ex != null)
            {
                MessageBox.Show(conn.Ex.Message);
            }
            else { MessageBox.Show("This event has bookings that must be deleted before continuing.."); }
        }
        private void Cancel()
        {
            IsReadOnly = true;
            if (SelectedModel == null) return;
            //Cancel new visit
            if (SelectedModel.ID == 0)
            {
                SourceModels.Remove(SourceModels.Last());
                SelectedModel = SourceModels.LastOrDefault();
            }
            //Cancel Edit Visit
            else if (dirtySelection != null)
            {
                SourceModels[SourceModels.GetCollectionIndex(SelectedModel.ID)] = dirtySelection;
                dirtySelection = null;
            }
        }
        #endregion

    }
}
