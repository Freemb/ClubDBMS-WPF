using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class MemberModel : ObservableObject
    {
        private double _memNo = 0;
        private string _forename = "";
        private string _surname = "";
        private string _category = "";
        private string _title = "";
        private string _email = "";
        private string _homeTel = "";
        private string _mobileTel = "";
        private string _gender = "";
        private DateTime _dateOfBirth = DateTime.Today.Date;

        public double MemNo { get => _memNo; set { OnPropertyChanged(ref _memNo, value); } }
        public string Forename { get => _forename; set { OnPropertyChanged(ref _forename, value); } }
        public string Surname { get => _surname; set { OnPropertyChanged(ref _surname, value); } }
        public string Title { get => _title; set { OnPropertyChanged(ref _title, value); } }
        public string Category { get => _category; set { OnPropertyChanged(ref _category, value); } }
        public string Email { get => _email; set { OnPropertyChanged(ref _email, value); } }
        public string HomeTel { get => _homeTel; set { OnPropertyChanged(ref _homeTel, value); } }
        public string MobileTel { get => _mobileTel; set { OnPropertyChanged(ref _mobileTel, value); } }
        public string Gender { get => _gender; set { OnPropertyChanged(ref _gender, value); } }
        public DateTime DateOfBirth { get => _dateOfBirth; set { OnPropertyChanged(ref _dateOfBirth, value); } }
        public MemberModel()
        {

        }
        public MemberModel(string memno, string title, string forename, string surname, string category, string email, string hometel, string mobiletel,
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
            return this.Forename + "  " + this.Surname;
        }
    }
}
