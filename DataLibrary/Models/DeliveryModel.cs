using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class DeliveryModel : ObservableModel, IModel<DeliveryModel>
    {
        private int? _iD = 0;
        private DateTime _entryTime = DateTime.Now;
        private DateTime? _exitTime = null;
        private string _vReg = "";
        private string _company = "";
        private string _make = "";
        private string _colour = "";
        private string _location = "";
        private string _driverName = "";
        private string _description = "";

        public int? ID { get => _iD; set => OnPropertyChanged(ref _iD, value); }
        public DateTime EntryTime { get => _entryTime; set => OnPropertyChanged(ref _entryTime , value); }
        public DateTime? ExitTime { get => _exitTime; set => OnPropertyChanged(ref  _exitTime , value); }
        public string VReg { get => _vReg; set => OnPropertyChanged(ref _vReg , value); }
        public string Company { get => _company; set => OnPropertyChanged(ref _company , value); }
        public string Make { get => _make; set => OnPropertyChanged(ref _make , value); }
        public string Colour { get => _colour; set => OnPropertyChanged(ref _colour, value); }
        public string Location { get => _location; set => OnPropertyChanged(ref _location , value); }
        public string DriverName { get => _driverName; set => OnPropertyChanged(ref _driverName , value); }
        public string Description { get => _description; set => OnPropertyChanged(ref _description , value); }

        public DeliveryModel()
        {

        }
        public DeliveryModel(int id, DateTime entry, DateTime? exit, string vreg, string company, string make, string colour, string location, 
                            string driver, string description)
        {
            ID = id;
            EntryTime = entry;
            ExitTime = exit;
            VReg = vreg;
            Company = company;
            Make = make;
            Colour = colour;
            Location = location;
            DriverName = driver;
            Description = description;
        }
       
        public DeliveryModel Clone(DeliveryModel model = null)
        {
            return (DeliveryModel)this.MemberwiseClone();
        }
    }
}
