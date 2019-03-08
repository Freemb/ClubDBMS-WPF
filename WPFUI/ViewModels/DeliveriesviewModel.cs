using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class DeliveriesViewModel : ObservableObject, IViewModel
    {
        private bool _isReadOnly;
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
            SourceModels = new ObservableCollection<DeliveryModel>(conn.Load(null, true));
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
            DeliveryModel temp = SourceModels.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            DeliveryModel temp = SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            DeliveryModel temp = SourceModels.Where((model) => model.ID < SelectedModel.ID).LastOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Next()
        {
            DeliveryModel temp = SourceModels.Where((model) => model.ID > SelectedModel.ID).FirstOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Add()
        {
            SourceModels.Add(new DeliveryModel());
            SelectedModel = SourceModels.Last();
            IsReadOnly = false;
        }
        private void Edit()
        {
            IsReadOnly = false;
            // dirtyDelivery = SelectedDelivery.MemberWiseClone();
        }
        private void Cancel()
        {
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
                    SourceModels[GetIndex(SelectedModel.ID)] = dirtySelection;
                    dirtySelection = null;
                }
            }
            IsReadOnly = true;
        }

        private void Delete()
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            throw new NotImplementedException();
        }
        private int GetIndex(int id)
        {
            DeliveryModel temp = SourceModels.Where((model) => model.ID == id).FirstOrDefault();
            return SourceModels.IndexOf(temp);
        }


    }  

}
