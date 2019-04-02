using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DataLibrary.Models;
using DataLibrary.Operations;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class EventBookingsViewModel : ObservableObject
    {
        private ObservableCollection<EventBookingModel> _sourcemodels;
        private EventBookingModel _selectedModel;
        private EventBookingModel dirtySelection;
        private bool _isReadOnly = true;

        public EventBookingModel SelectedModel { get => _selectedModel; set => OnPropertyChanged(ref _selectedModel, value); }
        public ObservableCollection<EventBookingModel> SourceModels
        {
            get { return _sourcemodels; }
            set { OnPropertyChanged(ref _sourcemodels, value); }
        }

        public bool IsEditMode { get => !IsReadOnly; }
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                OnPropertyChanged(ref _isReadOnly, value);
                OnPropertyChanged("IsEditMode");
            }
        }

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
        public ICommand LoadEventSpecCommand { get; private set; }
        #endregion

        public EventBookingsViewModel()
        {
            EventBookingConnector conn = new EventBookingConnector();
            //SourceModels = new ObservableCollection<EventBookingModel> (conn.Load(null, true));


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
            EventBookingModel temp = SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            if (SourceModels == null) return;
            EventBookingModel temp = SourceModels?.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            if (SourceModels == null) return;
            int index = SourceModels.GetCollectionIndex(SelectedModel?.ID);
            EventBookingModel temp = index - 1 > 0 ? SourceModels?[index - 1] : SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;


        }
        private void Next()
        {
            if (SourceModels == null) return;
            int index = SourceModels.GetCollectionIndex(SelectedModel?.ID);
            EventBookingModel temp = index + 1 < SourceModels.IndexOf(SourceModels.LastOrDefault()) ? SourceModels[index + 1] : SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Add()
        {
            if (SourceModels == null) return;
            SourceModels?.Add(new EventBookingModel());
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
                var conn = new EventBookingConnector();
                if (SelectedModel?.ID == 0)
                {
                    SelectedModel.ID = conn.Insert(SelectedModel);
                }
                else { conn.Update(SelectedModel); }
                if (conn.Ex == null)
                {
                    MessageBox.Show("Visit Added Successfully");
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
            EventBookingConnector conn = new EventBookingConnector();
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
            //Cancel new booking
            if (SelectedModel.ID == 0)
            {
                SourceModels.Remove(SourceModels.Last());
                SelectedModel = SourceModels.LastOrDefault();
            }
            //Cancel Edit booking
            else if (dirtySelection != null)
            {
                SourceModels[SourceModels.GetCollectionIndex(SelectedModel.ID)] = dirtySelection;
                dirtySelection = null;
            }
        }
        #endregion
    }
}
