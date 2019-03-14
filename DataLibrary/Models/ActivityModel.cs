using DataLibrary.Utility;
using System;

namespace DataLibrary.Models
{
    public class ActivityModel : ObservableModel,IEquatable<ActivityModel>
	{
		private string _activityName = "";
		private string _subActivity = "";
		private int _activityID = 0;
		private decimal _price = 0;
		private bool _isWEBH = false;
		private int? _subActivityID = null;
		private string _activityType = "";

		public string SubActivity
		{
			get => _subActivity;
			set{OnPropertyChanged(ref _subActivity, value);	}
		}
		public int? SubActivityID
		{
			get => _subActivityID;
            set => _subActivityID = value;
		}
		public string ActivityName
		{
			get => _activityName;
			set	{OnPropertyChanged(ref _activityName, value);}
		}
		public int ActivityID
		{
			get => _activityID;
            set => _activityID = value;
		}
		public string ActivityType
		{
			get => _activityType;
            set => _activityType = value;
		}
		public decimal Price
		{
			get => _price;
            set => _price = value;
		}
		public bool IsWEBH
		{
			get => _isWEBH;
            set => _isWEBH = value;
		}

       bool IEquatable<ActivityModel>.Equals(ActivityModel other)
       {
            return
                SubActivity.Equals(other.SubActivity) && ActivityName.Equals(other.ActivityName);


       }

        public override string ToString()
		{
			return ActivityName + "  |  " + SubActivity;
		}
        
	}


}
