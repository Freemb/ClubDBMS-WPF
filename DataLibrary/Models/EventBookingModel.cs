using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class EventBookingModel : ObservableModel, IModel<EventBookingModel>, IEquatable<EventBookingModel>
    {
        private int? _iD;

        public int? ID { get => _iD; set => _iD = value; }

        public EventBookingModel Clone(EventBookingModel model)
        {
            return (EventBookingModel)this.MemberwiseClone();
        }

        public bool Equals(EventBookingModel other)
        {
            throw new NotImplementedException();
        }
    }
}
