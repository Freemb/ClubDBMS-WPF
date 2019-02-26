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
using DataLibrary.Extensions;

namespace WPFUI.ViewModels
{
	public class MembersViewModel: ObservableObject
	{
		private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();
		private MemberModel _selectedmember = new MemberModel();
		public ObservableCollection<MemberModel> Members
		{
			get { return _members; }
			set { OnPropertyChanged(ref _members, value); } 
		}
		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set { OnPropertyChanged(ref _selectedmember, value); }
		}

		//constructor
		public MembersViewModel()//this(ShellViewModel.GetInstance)
		{
			Members = new ObservableCollection<MemberModel>(ShellViewModel.Softcache.Tables["Members"].ToMemberModelIEnum());
			SelectedMember = Members.First<MemberModel>();
		}
		
			   
	}
}
