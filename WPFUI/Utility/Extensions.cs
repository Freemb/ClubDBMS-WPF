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
			DataRow dr = memberstable.AsEnumerable().Where(datarow => datarow.Field<double>("MemNo") == memno).First();
			return new MemberModel
			{
				MemNo = dr.Field<double>("MemNo"),
				Forename = dr.Field<string>("Forename"),
				Surname = dr.Field<string>("Surname"),
				Category = dr.Field<string>("Category"),
				Email = dr.Field<string>("Email"),
				MobileTel = dr.Field<string>("MobileTel"),
				HomeTel = dr.Field<string>("HomeTel"),
				Gender = dr.Field<string>("Gender"),
				DateOfBirth = dr.IsNull("DOB") ? DateTime.MinValue : dr.Field<DateTime>("DOB")
			};
		}
		public static bool IsWeekendBankHoliday(this DateTime date)
		{
			if ((int)(date.DayOfWeek) < 5) return false;

			return true;
		}

	}
}
