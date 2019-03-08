using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
	public class GuestModel : ObservableModel
	{
		private string _forename = "";
		private string _surname = "";
		private string _email = "";

		public string Forename
		{
			get => _forename;
			set { OnPropertyChanged(ref _forename, value); }
		}
		public string Surname
		{
			get => _surname;
			set { OnPropertyChanged(ref _surname, value); }
		}
		public string Email
		{
			get => _email;
			set { OnPropertyChanged(ref _email, value); }
		}
		public override string ToString()
		{
			return Forename + " " + Surname;
		}
        public bool Equals(GuestModel obj)
        {
            return Forename.Equals(obj.Forename) && Surname.Equals(obj.Surname);
        }
    }
}
