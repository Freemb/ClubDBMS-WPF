using DataLibrary.Models;
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
    public class DeliveriesviewModel : ObservableObject
    {
        private bool _isReadOnly;
        private DeliveryModel _selectedDelivery = new DeliveryModel();
        private ObservableCollection<DeliveryModel> _deliveries;
        private DeliveryModel dirtyDelivery;

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
        public ObservableCollection<DeliveryModel> Deliveries
        { get => _deliveries;
          set => OnPropertyChanged(ref _deliveries , value); }
        public DeliveryModel SelectedDelivery
        {
            get { return _selectedDelivery; }
            set { OnPropertyChanged(ref _selectedDelivery, value); }
        }

        //Commands for binding to buttons/events
        public ICommand AddCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        //Navigation Bar Methods
        private void First()
        {
            DeliveryModel temp = Deliveries.FirstOrDefault();
            if (temp != null) SelectedDelivery = temp;
        }
        private void Last()
        {
            DeliveryModel temp = Deliveries.LastOrDefault();
            if (temp != null) SelectedDelivery = temp;
        }
        private void Previous()
        {
            DeliveryModel temp = Deliveries.Where((model) => model.ID < SelectedDelivery.ID).LastOrDefault();
            if (temp != null) SelectedDelivery = temp;

        }
        private void Next()
        {
            DeliveryModel temp = Deliveries.Where((model) => model.ID > SelectedDelivery.ID).FirstOrDefault();
            if (temp != null) SelectedDelivery = temp;

        }
        private void Add()
        {
            Deliveries.Add(new DeliveryModel());
            SelectedDelivery = Deliveries.Last();
            IsReadOnly = false;
        }
        private void Edit()
        {
            IsReadOnly = false;
            dirtyDelivery = SelectedDelivery.MemberWiseClone();
        }
        



    }
}
