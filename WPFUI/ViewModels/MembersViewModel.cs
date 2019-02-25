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
		private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();
		private MemberModel _selectedmember = new MemberModel();
		public ShellViewModel ShellPassed { get; private set; }

		public ObservableCollection<MemberModel> Members
		{
			get { return _members; }
			set { OnPropertyChanged(ref _members, value); } 
		}
		public MembersViewModel()//this(ShellViewModel.GetInstance)
		{
			
		}
		public MemberModel SelectedMember
		{
			get { return _selectedmember; }
			set	{ OnPropertyChanged(ref _selectedmember, value); }
		}

		public MembersViewModel(ShellViewModel shell)
		{
			this.ShellPassed = shell;
			MemberConnector mconn = new MemberConnector();
			Members = new ObservableCollection<MemberModel>(mconn.Load(null, true));
			SelectedMember = Members.First<MemberModel>();

		}

			   
	}
}
