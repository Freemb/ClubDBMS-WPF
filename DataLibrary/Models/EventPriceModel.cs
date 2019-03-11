using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class EventPriceModel:ObservableModel
    {
        private int? _eventspecid;
        private string _group;
        private decimal? _cost;

        public string Group
        {
             get { return _group; }
             set { OnPropertyChanged(ref _group, value); }
        }
        public decimal? Cost
        {
            get => _cost;
            set => OnPropertyChanged(ref _cost, value);
        }
        public int? EventSpecID
        {
            get => _eventspecid;
            set => OnPropertyChanged(ref _eventspecid, value);
        }
    }
}
