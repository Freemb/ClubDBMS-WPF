using WPFUI.Views;
using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
	public class MembersViewModel: ObservableObject
	{
		private double _memNo;
		private string _title;
		private string _forename;
		private string _surname;
		private string _category;
		private string _email;
		private string _mobiletel;
		private string _hometel;
		private string _gender;
		private DateTime _dob;
		private ObservableCollection<MemberModel> _members;
		private MemberModel _selectedmember;

		public RelayCommand	MyCommand { get; private set; }
		private MemberModel _memVM;

		public MemberModel MemVM
		{
			get { return _memVM; }
			set { OnPropertyChanged(ref _memVM, value); }
		}
		public double MemNo
		{
			get { return _memNo; }
			set
			{
				_memNo = value;
				OnPropertyChanged("MemNo");
			}
		}
		public string Title
		{
			get { return _title; }
			set
			{
				_title = value; //NotifyOfPropertyChange(() => Title); 
			}
		}
		public string Forename
		{
			get { return _forename; }
			set
			{
				_forename = value; //NotifyOfPropertyChange(() => Forename);
			}
		}
		public string Surname
		{
			get { return _surname; }
			set
			{
				_surname = value; //NotifyOfPropertyChange(() => Surname);
			}
		}
		public string Category
		{
			get { return _category; }
			set
			{
				_category = value; //NotifyOfPropertyChange(() => Category);
			}
		}
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value; //NotifyOfPropertyChange(() => Email); 
			}
		}
		public string MobileTel
		{
			get { return _mobiletel; }
			set
			{
				_mobiletel = value; //NotifyOfPropertyChange(() => MobileTel); 
			}
		}
		public string HomeTel
		{
			get { return _hometel; }
			set
			{
				_hometel = value; //NotifyOfPropertyChange(() => HomeTel); 
			}
		}
		public string Gender
		{
			get { return _gender; }
			set
			{
				_gender = value; //NotifyOfPropertyChange(() => Gender); 
			}
		}

		public DateTime DateOfBirth
		{
			get { return _dob; }
			set { _dob = value; //NotifyOfPropertyChange(() => DateOfBirth); 
			}
		}
		public ObservableCollection<MemberModel> Members
		{
			get { return _members; }
			set { OnPropertyChanged(ref _members, value); } 
		}
		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set	{ OnPropertyChanged(ref _selectedmember, value); }
		}

		public MembersViewModel()
		{
			MemberConnector mconn = new MemberConnector();
			Members = new ObservableCollection<MemberModel>(mconn.Load(null, true));
			SelectedMember = Members.First<MemberModel>();
			//MemNo = SelectedMember.MemNo;
			//Title = SelectedMember.Title;
			//Forename = SelectedMember.Forename;
			//Surname = SelectedMember.Surname;
			//Category = SelectedMember.Category;
			//Email = SelectedMember.Email;
			//MobileTel = SelectedMember.MobileTel;
			//HomeTel = SelectedMember.HomeTel;
			//Gender = SelectedMember.Gender;
			//DateOfBirth = SelectedMember.DateOfBirth;
			
		}

			   
	}
}
