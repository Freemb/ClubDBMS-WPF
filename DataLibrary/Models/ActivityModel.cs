using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Utility;

namespace DataLibrary.Models
{
	public class ActivityModel : ObservableObject
	{
		private string _activityName = "";
		private string _subActivity = "";

		public string SubActivity { get => _subActivity;
			set
			{
				OnPropertyChanged(ref _subActivity , value);
				
			}
		}
		public int SubActivityID { get; set; } = 0;
		public string ActivityName
		{
			get => _activityName;
			set
			{
				OnPropertyChanged(ref _activityName, value);
				
			}
		}
		public int ActivityID { get; set; } = 0;
		public string ActivityType { get; set; } = ""; //social, sporting, unrestricted, match, lesson
		public decimal Price { get; set; } = 0;
		public bool IsWEBH { get; set; } = false;


		public override string ToString()
		{
			return  ActivityName + "  |  " + SubActivity;
		}
	}

	
}
