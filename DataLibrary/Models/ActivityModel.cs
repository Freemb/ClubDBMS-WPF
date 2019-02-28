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
		private int _activityID = 0;
		private decimal _price = 0;
		private bool _isWEBH = false;
		private int _subActivityID = 0;
		private string _activityType = "";

		public string SubActivity
		{
			get => _subActivity;
			set{OnPropertyChanged(ref _subActivity, value);	}
		}
		public int SubActivityID
		{
			get => _subActivityID;
			set	{ OnPropertyChanged(ref _subActivityID, value); }
		}
		public string ActivityName
		{
			get => _activityName;
			set	{OnPropertyChanged(ref _activityName, value);}
		}
		public int ActivityID
		{
			get => _activityID;
			set	{OnPropertyChanged(ref _activityID, value);	}
		}
		public string ActivityType
		{
			get => _activityType;
			set {OnPropertyChanged(ref _activityType , value);}
		}
		public decimal Price
		{
			get => _price;
			set	{OnPropertyChanged(ref _price, value);}
		}
		public bool IsWEBH
		{
			get => _isWEBH;
			set{OnPropertyChanged(ref _isWEBH, value);}
		}

		public override string ToString()
		{
			return ActivityName + "  |  " + SubActivity;
		}
	}


}
