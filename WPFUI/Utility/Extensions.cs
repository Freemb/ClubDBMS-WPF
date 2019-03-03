using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;

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
		public static MemberModel GetMemberDetails(this DataTable memberstable, double memno)
		{
            ////This gives an error if membership number not found, used foreach instead
			//DataRow row = memberstable.AsEnumerable().Where(datarow => datarow.Field<double>("MemNo") == memno).First();
            foreach (var row in memberstable.AsEnumerable())
            {
                
                if(row.Field<double>("MemNo") == memno) return new MemberModel
                {
                        MemNo = row.Field<double>("MemNo"),
                        Forename = row.Field<string>("Forename"),
                        Surname = row.Field<string>("Surname"),
                        Category = row.Field<string>("Category"),
                        Email = row.Field<string>("Email"),
                        MobileTel = row.Field<string>("MobileTel"),
                        HomeTel = row.Field<string>("HomeTel"),
                        Gender = row.Field<string>("Gender"),
                        DateOfBirth = row.IsNull("DOB") ? DateTime.MinValue : row.Field<DateTime>("DOB")
                };
                
            }
            return new MemberModel();
        }
		public static bool IsWeekendBankHoliday(this DateTime date)
		{
			if ((int)(date.DayOfWeek) < 5) return false;
            //TODO institute checking of list of BH

			return true;
		}

	}
}
