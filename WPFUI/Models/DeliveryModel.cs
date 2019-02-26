using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Utility;

namespace WPFUI.Models
{
	public class DeliveryModel:ObservableObject
	{
		public int ID { get; set; }
		public DateTime EntryTime { get; set; }
		public DateTime ExitTime { get; set; }
		public string VReg { get; set; }
		public string Company { get; set; }
		public string Make { get; set; }
		public string Colour { get; set; }
		public string Location { get; set; }
		public string DriverName { get; set; }
		public string Description { get; set; }

	}
}
