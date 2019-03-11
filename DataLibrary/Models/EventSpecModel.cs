using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Utility;

namespace DataLibrary.Models
{
    public class EventSpecModel : ObservableModel
    {
        private int? _id = 0;
        private DateTime? _eventdate;
        private string _location;
        private short? _maxLimit;
        private List<EventPriceModel> _pricelist = new List<EventPriceModel>();
        private EventModel _event = new EventModel();

        public EventModel Event { get => _event; set => OnPropertyChanged(ref _event , value); }
        public int? ID
        {
            get { return _id; }
            set { OnPropertyChanged(ref _id, value); }
        }
        public DateTime? EventDate
        {
            get { return _eventdate; }
            set { OnPropertyChanged(ref _eventdate, value); }
        }
        public string Location
        {
            get { return _location; }
            set { OnPropertyChanged(ref _location, value); }
        }
        public short? MaxLimit
        {
            get { return _maxLimit; }
            set { OnPropertyChanged(ref _maxLimit, value); }
        }
        public List<EventPriceModel> PriceList
        {
            get => _pricelist;
            set => OnPropertyChanged(ref _pricelist, value);
        }

        public EventSpecModel()
        {

        }

        public EventSpecModel(int eventid, int? eventspecid, DateTime? eventdate, string location, short? maxlimit, string name, string type, string freq, string mode)
        {
            ID = eventspecid;
            EventDate = eventdate;
            Location = location;
            MaxLimit = maxlimit;
            Event.ID = eventid;
            Event.EventName = name;
            Event.Type = type;
            Event.Frequency = freq;
            Event.Mode = mode;
        }
        public EventSpecModel Clone()
        {
            return (EventSpecModel)this.MemberwiseClone();
        }
    }
}
