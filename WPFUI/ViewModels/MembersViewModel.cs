using DataLibrary.Cache;
using DataLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;
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
			SourceModels = new ObservableCollection<MemberModel>(CacheOps.GetFromCache<MemberModel>("Members"));
            SelectedModel = SourceModels.FirstOrDefault();
		}
		
			   
	}
}
