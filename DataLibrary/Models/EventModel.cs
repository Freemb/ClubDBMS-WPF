using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class EventModel :ObservableModel
    {
        private int _id;
        private string _eventName;
        private string _type;
        private string _frequency;
        private string _mode;

        public int ID
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






    }
}
