﻿using DataLibrary.Utility;
using System;

namespace DataLibrary.Models
{
    public class MemberModel : ObservableModel, IModel<MemberModel>
    {
        private int? _id = 0;
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

        public int? ID { get => _id; set => _id = value; } // introduced to satisfy implementation of IModel, not present in data source
        public double MemNo
        {
            get => _memNo;
            set
            {
               if(OnPropertyChanged(ref _memNo, value))
               {
                   _id = (int)(value * 10D);
                    OnPropertyChanged("ID");
               }
                
            }
        }
        public string Forename { get => _forename; set => OnPropertyChanged(ref _forename, value);}
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
        public override bool Equals(object obj)
        {
            MemberModel temp = obj as MemberModel;
            if (temp == null)
            {
                return false;
            }
            else
            {
                return MemNo == temp.MemNo &&
                    Title == temp.Title &&
                    Forename == temp.Forename &&
                    Surname == temp.Surname &&
                    Category == temp.Category &&
                    Email == temp.Email &&
                    HomeTel == temp.HomeTel &&
                    MobileTel == temp.MobileTel &&
                    Gender == temp.Gender &&
                    DateOfBirth == temp.DateOfBirth;


            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return this.Forename + "  " + this.Surname;
        }

        public MemberModel Clone(MemberModel model = null)
        {
            return (MemberModel)this.MemberwiseClone();
        }
    }
}
