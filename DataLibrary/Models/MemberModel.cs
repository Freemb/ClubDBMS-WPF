using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class MemberModel:ObservableObject
    {
		public double MemNo { get; set; } = 0;
		public string Forename { get; set; } = "";
		public string Surname { get; set; } = "";
		public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Email { get; set; } = "";

		public string HomeTel { get; set; } = "";
		public string MobileTel { get; set; } = "";
		public string Gender { get; set; } = "";
		public DateTime DateOfBirth { get; set; } = DateTime.Today.Date;

		public MemberModel()
        {

        }
        public MemberModel(string memno,string title, string forename, string surname, string category, string email, string hometel, string mobiletel,
            string gender, string dob)
        {
            double.TryParse(memno, out double memnoval);
            MemNo = memnoval;

            Title = title;
            Forename = forename;
            Surname = surname;
            Category = category;
            Email = email;
            HomeTel = hometel;
            MobileTel = mobiletel;
            Gender = gender;

            DateTime.TryParse(dob, out DateTime DOBValue);
            DateOfBirth = DOBValue.Date;

        }
		public override string ToString()
		{
			return this.Forename + "  " + this.Surname ;
		}
	}
}
