using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class VisitModel : ObservableModel, IEquatable<VisitModel>
    {
        private ActivityModel _activity;
        private DateTime _visitDate = DateTime.Now.Date;
        private MemberModel _member;
        private int _visitID = 0;
        private GuestModel _guest;
        private decimal? _amount = null;
        private bool _isPaid = false;
        private string _notes = "";
        private DateTime? _paidDate = null;

        public int ID { get => _visitID; set => OnPropertyChanged(ref _visitID, value);}
        public DateTime VisitDate { get => _visitDate; set => OnPropertyChanged(ref _visitDate, value);}
        public MemberModel Member { get => _member; set => OnPropertyChanged(ref _member, value); }
        public GuestModel Guest { get => _guest; set => OnPropertyChanged(ref _guest, value);}
        public ActivityModel Activity
        {
            get{return _activity;}
            set{OnPropertyChanged(ref _activity, value);}
        }

        public decimal? Amount { get => _amount; set => OnPropertyChanged(ref _amount, value);}
        public bool IsPaid { get => _isPaid; set => OnPropertyChanged(ref _isPaid, value); }
        public DateTime? PaidDate { get => _paidDate; set => OnPropertyChanged(ref _paidDate, value);}
        public string Notes { get => _notes; set => OnPropertyChanged(ref _notes, value);}
       
        //constructors
        public VisitModel()
        {
            Member = new MemberModel();
            Activity = new ActivityModel();
            Guest = new GuestModel();
        }

        public VisitModel(int id, DateTime VDate, double memno, string memfore, string memsur, string category, int? activityID, string activityname, string subactivity,
                            decimal? price, DateTime? Pdate,bool ispaid, string GFore, string GSur)
        {
            Member = new MemberModel { Forename = memfore, Surname = memsur, Category = category, MemNo = memno };
            Guest = new GuestModel { Forename = GFore, Surname = GSur };
            Activity = new ActivityModel { ActivityName = activityname, SubActivity = subactivity, SubActivityID = activityID };
            VisitDate = VDate;
            ID = id;
            Amount = price;
            IsPaid = ispaid;
            PaidDate = Pdate;
        }


        //out-prioritizes equals method when used with IEqualityComparer<T>, i.e custom equality test
        bool IEquatable<VisitModel>.Equals(VisitModel other)
        {
            return
                ID.Equals(other.ID) &&
                VisitDate.Equals(other.VisitDate) &&
                Amount.Equals(other.Amount) &&
                IsPaid.Equals(other.IsPaid) &&
                PaidDate.Equals(other.PaidDate) &&
                Notes.Equals(other.Notes) &&
                Activity.Equals(other.Activity) &&
                Member.MemNo.Equals(other.Member.MemNo) &&
                Guest.Equals(other.Guest);


        }
        public VisitModel Clone(VisitModel input)
        {
            return new VisitModel
                (
                input._visitID,
                input._visitDate,
                input.Member.MemNo,
                input.Member.Forename,
                input.Member.Surname,
                input.Member.Category,
                input._activity.ActivityID,
                input.Activity.ActivityName,
                input.Activity.SubActivity,
                input._amount,
                input._paidDate,
                input._isPaid,
                input._guest.Forename,
                input._guest.Surname
                );
                
                

            
        }
    }
}
