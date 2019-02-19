using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class VisitModel
    {
		
		public int VisitID { get; set; } = 0;
		public DateTime VisitDate { get; set; } = DateTime.Now.Date;

		public double MemNo { get; set; }
        public GuestModel Guest { get; set; }
        public ActivityModel Activity { get; set; }
		public decimal Amount { get; set; } = 0;
		public bool IsPaid { get; set; } = false;
		public DateTime PaidDate { get; set; } = DateTime.Now.Date;
		public string Notes { get; set; } = "";

        public VisitModel()
        {
			Activity = new ActivityModel();
			Guest = new GuestModel();
        }

        public VisitModel(string id,string VDate, string memno, string activityID, string activityname, string subactivity = "", 
							string price = "", string Pdate = "", string GFore= "", string GSur = "")
        {
            Activity = new ActivityModel { ActivityName = activityname, SubActivity = subactivity };
            int.TryParse(activityID, out int ActivityIDvalue);
            Activity.SubActivityID = ActivityIDvalue;
           
            DateTime VisitdateValue = DateTime.Today;
            DateTime.TryParse(VDate, out VisitdateValue);
            VisitDate = VisitdateValue;
			int.TryParse(id, out int idValue);
			VisitID = idValue;

            double.TryParse(memno, out double MemNoValue);
			MemNo = MemNoValue;
			
            decimal.TryParse(price, out decimal AmountValue);
            Amount = AmountValue;

			if (DateTime.TryParse(Pdate, out DateTime PaidDateValue))
			{
				IsPaid = true;
				PaidDate = PaidDateValue;
			}
			Guest = new GuestModel {Forename = GFore,Surname = GSur};
            
        }
    }
}
