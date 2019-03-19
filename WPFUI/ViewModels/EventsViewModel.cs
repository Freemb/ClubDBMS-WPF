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
    public class EventsViewModel : ObservableObject
    {

        private ObservableCollection<EventSpecModel> _sourcemodels;
        private EventSpecModel _selectedModel;
        private EventModel _selectedEvent;
        private EventSpecModel dirtySelection;
        private bool _isReadOnly = true;
        private ObservableCollection<EventModel> _rootEvents;

        public EventSpecModel SelectedModel { get => _selectedModel; set => OnPropertyChanged(ref _selectedModel, value); }
        public EventModel SelectedEvent { get => _selectedEvent; set => OnPropertyChanged(ref _selectedEvent, value); }
        public ObservableCollection<EventSpecModel> SourceModels
        {
            get { return _sourcemodels; }
            set { OnPropertyChanged(ref _sourcemodels, value); }
        }
        public ObservableCollection<EventModel> RootEvents { get => _rootEvents; set => OnPropertyChanged(ref _rootEvents , value); }


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

        public EventsViewModel()
        {
            
            EventConnector econ = new EventConnector();
            RootEvents = new ObservableCollection<EventModel>(econ.Load(null, true));
            _selectedEvent = RootEvents.FirstOrDefault();
            
            LastCommand = new RelayCommand(Last, () => IsReadOnly);
            FirstCommand = new RelayCommand(First, () => IsReadOnly);
            PreviousCommand = new RelayCommand(Previous, () => IsReadOnly);
            NextCommand = new RelayCommand(Next, () => IsReadOnly);
            AddCommand = new RelayCommand(Add, () => IsReadOnly);
            SaveCommand = new RelayCommand(Save, () => IsEditMode);
            EditCommand = new RelayCommand(Edit, () => IsReadOnly);
            DeleteCommand = new RelayCommand(Delete, () => IsReadOnly);
            CancelCommand = new RelayCommand(Cancel, () => IsEditMode);
            LoadEventSpecCommand = new RelayCommand(LoadEventSpec, () => true);
        }
        #region Navigation Bar Methods
        private void First()
        {
            if (SourceModels == null) return;
            EventSpecModel temp = SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            if (SourceModels == null) return;
            EventSpecModel temp = SourceModels?.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            if (SourceModels == null) return;
            int index = GetIndex(SelectedModel?.ID);
            EventSpecModel temp = index - 1 > 0 ? SourceModels?[index - 1] : SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;


        }
        private void Next()
        {
            if (SourceModels == null) return;
            int index = GetIndex(SelectedModel?.ID);
            EventSpecModel temp = index + 1 < SourceModels.IndexOf(SourceModels.LastOrDefault()) ? SourceModels[index + 1] : SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Add()
        {
            if (SourceModels == null) return;
            SourceModels?.Add(new EventSpecModel());
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
                EventSpecConnector conn = new EventSpecConnector();
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
            EventSpecConnector conn = new EventSpecConnector();
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
                    SourceModels[GetIndex(SelectedModel.ID)] = dirtySelection;
                    dirtySelection = null;
                }
        }
        #endregion
        private int GetIndex(int? ID) // returns -1 if ID is null.
        {
            if (SourceModels == null) return 0;
            EventSpecModel temp = SourceModels.Where(model => model.ID == ID).FirstOrDefault();
            return SourceModels.IndexOf(temp);
        }
        private void LoadEventSpec()
        {
            EventSpecConnector esconn = new EventSpecConnector();
            SourceModels = new ObservableCollection<EventSpecModel>(esconn.Load(SelectedEvent.ID.ToString(), false));
            SelectedModel = SourceModels.FirstOrDefault();
        }
    }
}
