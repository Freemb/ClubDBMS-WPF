using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class VisitModel : ObservableObject, IEquatable<VisitModel>
    {
        private ActivityModel _activity;
        private DateTime _visitDate = DateTime.Now.Date;
        private MemberModel _member;
        private int _visitID = 0;
        private GuestModel _guest;
        private decimal _amount = 0;
        private bool _isPaid = false;
        private string _notes = "";
        private DateTime _paidDate = DateTime.Now.Date;

        public int VisitID { get => _visitID; set { OnPropertyChanged(ref _visitID, value); } }
        public DateTime VisitDate { get => _visitDate; set { OnPropertyChanged(ref _visitDate, value); } }
        public MemberModel Member { get => _member; set { OnPropertyChanged(ref _member, value); } }
        public GuestModel Guest { get => _guest; set { OnPropertyChanged(ref _guest, value); } }
        public ActivityModel Activity
        {
            get
            {
                return _activity;
            }
            set
            {
                //if (value != null) // temporary fix for bug where null introduced after selecting from combo box
                    OnPropertyChanged(ref _activity, value);

            }
        }

        public decimal Amount { get => _amount; set { OnPropertyChanged(ref _amount, value); } }
        public bool IsPaid { get => _isPaid; set { OnPropertyChanged(ref _isPaid, value); } }
        public DateTime PaidDate { get => _paidDate; set { OnPropertyChanged(ref _paidDate, value); } }
        public string Notes { get => _notes; set { OnPropertyChanged(ref _notes, value); } }
        public VisitModel()
        {
            Member = new MemberModel();
            Activity = new ActivityModel();
            Guest = new GuestModel();
        }

        public VisitModel(string id, string VDate, string memno, string memfore, string memsur, string category, string activityID, string activityname, string subactivity = "",
                            string price = "", string Pdate = "", string GFore = "", string GSur = "")
        {
            Member = new MemberModel { Forename = memfore, Surname = memsur, Category = category };
            double.TryParse(memno, out double MemNoValue);
            Member.MemNo = MemNoValue;

            Activity = new ActivityModel { ActivityName = activityname, SubActivity = subactivity };
            int.TryParse(activityID, out int ActivityIDvalue);
            Activity.SubActivityID = ActivityIDvalue;

            DateTime VisitdateValue = DateTime.Today;
            DateTime.TryParse(VDate, out VisitdateValue);
            VisitDate = VisitdateValue;
            int.TryParse(id, out int idValue);
            VisitID = idValue;



            decimal.TryParse(price, out decimal AmountValue);
            Amount = AmountValue;

            if (DateTime.TryParse(Pdate, out DateTime PaidDateValue))
            {
                IsPaid = true;
                PaidDate = PaidDateValue;
            }
            Guest = new GuestModel { Forename = GFore, Surname = GSur };

        }


        //overwrites equals method when used with IEqualityComparer<T>, i.e custom equality test
        bool IEquatable<VisitModel>.Equals(VisitModel other)
        {
            return
                VisitID.Equals(other.VisitID) &&
                VisitDate.Equals(other.VisitDate) &&
                Amount.Equals(other.Amount) &&
                IsPaid.Equals(other.IsPaid) &&
                PaidDate.Equals(other.PaidDate) &&
                Notes.Equals(other.Notes) &&
                Activity.Equals(other.Activity) &&
                Member.MemNo.Equals(other.Member.MemNo) &&
                Guest.Equals(other.Guest);


        }
    }
}
