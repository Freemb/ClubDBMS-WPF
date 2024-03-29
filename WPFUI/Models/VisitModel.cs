﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Utility;

namespace WPFUI.Models
{
	public class VisitModel :ObservableObject
	{

		public int VisitID { get; set; } = 0;
		public DateTime VisitDate { get; set; } = DateTime.Now.Date;

		public MemberModel Member { get; set; }
		public GuestModel Guest { get; set; }
		public ActivityModel Activity { get; set; }
		public decimal Amount { get; set; } = 0;
		public bool IsPaid { get; set; } = false;
		public DateTime PaidDate { get; set; } = DateTime.Now.Date;
		public string Notes { get; set; } = "";

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

	}
}
