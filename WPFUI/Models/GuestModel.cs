using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Utility;

namespace WPFUI.Models
{
	public class GuestModel:ObservableObject
	{
		public string Forename { get; set; } = "";

		public string Surname { get; set; } = "";
		public string Email { get; set; } = "";

		public override string ToString()
		{
			return Forename + " " + Surname;
		}
	}
}
