using DataLibrary.Utility;
using System;

namespace DataLibrary.Models
{
    public class EventModel :ObservableModel, IEquatable<EventModel>, IModel<EventModel>
    {
        private int? _id;
        private string _eventName;
        private string _type;
        private string _frequency;
        private string _mode;

        public int? ID
        {
            get { return _id; }
            set {OnPropertyChanged(ref _id , value); }
        }
        public string EventName
        {
            get { return _eventName; }
            set {OnPropertyChanged(ref _eventName , value); }
        }
        public string Type
        {
            get { return _type; }
            set {OnPropertyChanged(ref _type , value); }
        }
        public string Frequency
        {
            get { return _frequency; }
            set {OnPropertyChanged(ref _frequency , value); }
        }
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public EventModel Clone(EventModel model)
        {
            return (EventModel)this.MemberwiseClone();
        }

        public bool Equals(EventModel other)
        {
            if(_id.Equals(other.ID) && _eventName.Equals(other.EventName) && _type.Equals(other.Type) && _frequency.Equals(other.Frequency) && _mode.Equals(other.Mode))
            { return true; }
            else{ return false; }
            
        }
    }
}
