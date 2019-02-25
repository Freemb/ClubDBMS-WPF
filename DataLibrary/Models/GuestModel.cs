using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class GuestModel
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
