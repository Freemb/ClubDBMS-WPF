﻿using WPFUI.Views;
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
        private bool _isReadOnly;

        private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();
		private MemberModel _selectedmember = new MemberModel();
		public ObservableCollection<MemberModel> SourceModels
		{
			get { return _members; }
			set { OnPropertyChanged(ref _members, value); } 
		}
		public MemberModel SelectedModel
		{
			get { return _selectedmember; }
			set { OnPropertyChanged(ref _selectedmember, value); }
		}

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                OnPropertyChanged(ref _isReadOnly, value);
                OnPropertyChanged("IsEditMode");
            }

        }
        public bool IsEditMode

        {
            get { return !_isReadOnly; }

        }

        //constructor
        public MembersViewModel()
		{
			SourceModels = new ObservableCollection<MemberModel>(ShellViewModel.Softcache.Tables["Members"].ToMemberModelIEnum());
			SelectedModel = SourceModels.FirstOrDefault();
		}
		
			   
	}
}
