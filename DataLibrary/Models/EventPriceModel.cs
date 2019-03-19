using DataLibrary.Utility;

namespace DataLibrary.Models
{
    public class EventPriceModel:ObservableModel, IModel<EventPriceModel>
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
        public int? ID
        {
            get => _eventspecid;
            set => OnPropertyChanged(ref _eventspecid, value);
        }

        public EventPriceModel Clone(EventPriceModel model)
        {
            return (EventPriceModel)this.MemberwiseClone();
        }
    }
}
