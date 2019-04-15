using DataLibrary.Utility;
using System;

namespace DataLibrary.Models
{
    public class EventBookingModel : ObservableModel, IModel<EventBookingModel>, IEquatable<EventBookingModel>
    {
        private int? _iD;
        private string _bookingRef;
        private int _eventSpecID;
        private DateTime _bookingTime;
        private MemberModel _member = new MemberModel();
        private string _contactNo;
        private short _tableNo;
        private string _tablePos;
        private short _numPeople;
        private string _requirements;
        private bool _isConfirmed;

        public int? ID { get => _iD; set => _iD = value; }
        public string BookingRef { get => _bookingRef; set => OnPropertyChanged(ref _bookingRef , value); }
        public int EventSpecID { get => _eventSpecID; set => OnPropertyChanged(ref _eventSpecID , value); }
        public DateTime BookingTime { get => _bookingTime; set => OnPropertyChanged(ref _bookingTime ,value); }
        public MemberModel Member { get => _member; set => OnPropertyChanged(ref _member ,value); }
        public string ContactNo { get => _contactNo; set => _contactNo = value; }
        public short TableNo { get => _tableNo; set => OnPropertyChanged(ref _tableNo , value); }
        public string TablePos { get => _tablePos; set => _tablePos = value; }
        public short NumPeople { get => _numPeople; set => _numPeople = value; }
        public string Requirements { get => _requirements; set => _requirements = value; }
        public bool IsConfirmed { get => _isConfirmed; set => OnPropertyChanged(ref _isConfirmed ,value); }

        public EventBookingModel()
        {

        }
        

        public EventBookingModel(string bookref, int eventspecid, DateTime booktime, double memno, string contactno, short tableno, string tablepos, short numppl,
                                    string reqs, bool isconf)
        {
            BookingRef = bookref;
            EventSpecID = eventspecid;
            BookingTime = booktime;
            ContactNo = contactno;
            TableNo = tableno;
            TablePos = tablepos;
            NumPeople = numppl;
            Requirements = reqs;
            IsConfirmed = isconf;
            Member.MemNo = memno;
        }

        public EventBookingModel Clone(EventBookingModel model = null)
        {
            return (EventBookingModel)this.MemberwiseClone();
        }

        public bool Equals(EventBookingModel other)
        {
            return BookingRef == other.BookingRef &&
                    EventSpecID == other.EventSpecID &&
                    BookingTime == other.BookingTime &&
                    Member.Equals(other.Member) &&
                    TableNo == other.TableNo &&
                    ContactNo == other.ContactNo &&
                    Requirements == other.Requirements &&
                    NumPeople == other.NumPeople &&
                    IsConfirmed == other.IsConfirmed;
        }
    }
}
