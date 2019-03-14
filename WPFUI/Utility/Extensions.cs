using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WPFUI.Utility
{
    public static class ExtensionMethods
	{
		public static IEnumerable<MemberModel> ToMemberModelIEnum(this DataTable table)
		{
			return table.AsEnumerable().Select(row => new MemberModel()
			{
				MemNo = row.Field<double>("MemNo"),
				Title = row.Field<string>("Title"),
				Forename = row.Field<string>("Forename"),
				Surname = row.Field<string>("Surname"),
				Category = row.Field<string>("Category"),
				Email = row.Field<string>("Email"),
				MobileTel = row.Field<string>("MobileTel"),
				HomeTel = row.Field<string>("HomeTel"),
				Gender = row.Field<string>("Gender"),
				DateOfBirth = row.IsNull("DOB") ? DateTime.MinValue : row.Field<DateTime>("DOB")
			});

		}
		public static IEnumerable<ActivityModel> ToActivityModelIEnum(this DataTable table)
		{
			return table.AsEnumerable().Select(row => new ActivityModel()
			{
				SubActivityID = row.Field<int>("SubActivityId"),
				ActivityID = Convert.ToInt32(row.Field<object>("ActivityId")),
				ActivityName = row.Field<string>("Activity"),
				SubActivity = row.Field<string>("SubActivity"),
				ActivityType = row.Field<string>("Type"),
				Price = row.Field<decimal>("Price"),
				IsWEBH = row.Field<bool>("WkBH")
			});


		}
		public static MemberModel GetMemberDetails(this List<MemberModel> memlist, double memno)
		{
           MemberModel member = memlist.Where(model => model.MemNo == memno).FirstOrDefault();
           return member;
        }
		public static bool IsWeekendBankHoliday(this DateTime date)
		{
			if ((int)(date.DayOfWeek) < 5) return false;
            //TODO institute checking of list of BH

			return true;
		}
        public static VisitModel CloneEx(this VisitModel input)
        {
            return new VisitModel
                (
                input.ID,
                input.VisitDate,
                input.Member.MemNo,
                input.Member.Forename,
                input.Member.Surname,
                input.Member.Category,
                input.Activity.ActivityID,
                input.Activity.ActivityName,
                input.Activity.SubActivity,
                input.Amount,
                input.PaidDate,
                input.IsPaid,
                input.Guest.Forename,
                input.Guest.Surname
                );
        }

      
    }
}
