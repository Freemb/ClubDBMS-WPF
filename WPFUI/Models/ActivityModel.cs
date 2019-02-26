using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Utility;

namespace WPFUI.Models
{
	public class ActivityModel:ObservableObject
	{
		public string SubActivity { get; set; } = "";
		public int SubActivityID { get; set; } = 0;
		public string ActivityName { get; set; } = "";
		public int ActivityID { get; set; } = 0;
		public string ActivityType { get; set; } = ""; //social, sporting, unrestricted, match, lesson
		public decimal Price { get; set; } = 0;
		public bool IsWEBH { get; set; } = false;


	}
}
