using Caliburn.Micro;
using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaWPFUI.ViewModels
{
	public class MembersViewModel : Screen
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
		private BindableCollection<MemberModel> _members;
		private MemberModel _selectedmember;

		public double MemNo
		{
			get { return _memNo; }
			set { _memNo = value; NotifyOfPropertyChange(() => MemNo); }
		}
		public string Title
		{
			get { return _title; }
			set { _title = value; NotifyOfPropertyChange(() => Title); }
		}
		public string Forename
		{
			get { return _forename; }
			set { _forename = value; NotifyOfPropertyChange(() => Forename); }
		}
		public string Surname
		{
			get { return _surname; }
			set { _surname = value; NotifyOfPropertyChange(() => Surname); }
		}
		public string Category
		{
			get { return _category; }
			set { _category = value; NotifyOfPropertyChange(() => Category); }
		}
		public string Email
		{
			get { return _email; }
			set { _email = value; NotifyOfPropertyChange(() => Email); }
		}
		public string MobileTel
		{
			get { return _mobiletel; }
			set { _mobiletel = value; NotifyOfPropertyChange(() => MobileTel); }
		}
		public string HomeTel
		{
			get { return _hometel; }
			set { _hometel = value; NotifyOfPropertyChange(() => HomeTel); }
		}
		public string Gender
		{
			get { return _gender; }
			set { _gender = value; NotifyOfPropertyChange(() => Gender); }
		}
		public DateTime DateOfBirth
		{
			get { return _dob; }
			set { _dob = value; NotifyOfPropertyChange(() => DateOfBirth); }
		}
		public BindableCollection<MemberModel> Members
		{
			get { return _members; }
			set { _members = value; NotifyOfPropertyChange(() => Members); }
		}
		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set
			{
				_selectedmember = value;
				//NotifyOfPropertyChange(() => SelectedMember); //this causes undesired member model reads of all records. needed if binding to selected member in text boxes
			}
		}

		public MembersViewModel()
		{
			MemberConnector mconn = new MemberConnector();
			Members = new BindableCollection<MemberModel>(mconn.Load(null, true));
			SelectedMember = Members.First<MemberModel>();
			MemNo = SelectedMember.MemNo;
			Title = SelectedMember.Title;
			Forename = SelectedMember.Forename;
			Surname = SelectedMember.Surname;
			Category = SelectedMember.Category;
			Email = SelectedMember.Email;
			MobileTel = SelectedMember.MobileTel;
			HomeTel = SelectedMember.HomeTel;
			Gender = SelectedMember.Gender;
			DateOfBirth = SelectedMember.DateOfBirth;
			
		}













	}
}
