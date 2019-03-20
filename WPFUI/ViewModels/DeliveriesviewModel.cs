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
    public class DeliveriesViewModel : ObservableObject
    {
        private bool _isReadOnly = true;
        private DeliveryModel _selectedDelivery = new DeliveryModel();
        private ObservableCollection<DeliveryModel> _deliveries;
        private DeliveryModel dirtySelection;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                OnPropertyChanged(ref _isReadOnly, value);
                OnPropertyChanged("IsEditMode");
            }

        }
        public bool IsEditMode

        {
            get { return !_isReadOnly; }

        }

        //properties for binding to View
        public ObservableCollection<DeliveryModel> SourceModels
        {
            get => _deliveries;
            set => OnPropertyChanged(ref _deliveries, value);
        }
        public DeliveryModel SelectedModel
        {
            get { return _selectedDelivery; }
            set { OnPropertyChanged(ref _selectedDelivery, value); }
        }

        //Command properties for binding to buttons/events
        public ICommand AddCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        //Constructor
        public DeliveriesViewModel()
        {
            //Load Deliveries data from database and assign to observable collection for binding
            DeliveryConnector conn = new DeliveryConnector();
            SourceModels = new ObservableCollection<DeliveryModel>(conn.Load());
            SelectedModel = SourceModels.FirstOrDefault();

            //Assign Commands for Binding
            LastCommand = new RelayCommand(Last, () => IsReadOnly);
            FirstCommand = new RelayCommand(First, () => IsReadOnly);
            PreviousCommand = new RelayCommand(Previous, () => IsReadOnly);
            NextCommand = new RelayCommand(Next, () => IsReadOnly);
            AddCommand = new RelayCommand(Add, () => IsReadOnly);
            SaveCommand = new RelayCommand(Save, () => IsEditMode);
            EditCommand = new RelayCommand(Edit, () => IsReadOnly);
            DeleteCommand = new RelayCommand(Delete, () => IsReadOnly);
            CancelCommand = new RelayCommand(Cancel, () => IsEditMode);

        }



        //Navigation Bar Methods
        private void First()
        {
            if (SourceModels == null) return;
            DeliveryModel temp = SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            if (SourceModels == null) return;
            DeliveryModel temp = SourceModels?.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            if (SourceModels == null) return;
            int index = SourceModels.GetCollectionIndex(SelectedModel?.ID);
            DeliveryModel temp = index - 1 > 0 ? SourceModels?[index - 1] : SourceModels?.FirstOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Next()
        {
            if (SourceModels == null) return;
            int index = SourceModels.GetCollectionIndex(SelectedModel?.ID);
            DeliveryModel temp = index + 1 < SourceModels.IndexOf(SourceModels.LastOrDefault()) ? SourceModels[index + 1] : SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Add()
        {
            if (SourceModels == null) return;
            SourceModels?.Add(new DeliveryModel());
            SelectedModel = SourceModels?.LastOrDefault();
            IsReadOnly = false;
        }
        private void Edit()
        {
            if (SourceModels == null) return;
            IsReadOnly = false;
            dirtySelection = SelectedModel.Clone();
        }
        private void Cancel()
        {
            IsReadOnly = true;
            if (SelectedModel != null)
            {
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
        }
        private void Delete()
        {
            {
                if (SelectedModel == null) return;
                DeliveryConnector conn = new DeliveryConnector();
                //Delete call to Datasource does not run unless ID !=0
                if (SelectedModel.ID == 0 || conn.Delete(SelectedModel)) 
                {
                    SourceModels.Remove(SelectedModel);
                    SelectedModel = SourceModels.LastOrDefault();
                    MessageBox.Show("Entry Deleted Successfully");
                    return;
                }
                if (conn.Ex != null)
                {
                    MessageBox.Show(conn.Ex.Message);
                }
                else { MessageBox.Show("Something went wrong oopsie.."); }
            }
        }

        private void Save()
        {
            throw new NotImplementedException();
        }

       
    }  

}
